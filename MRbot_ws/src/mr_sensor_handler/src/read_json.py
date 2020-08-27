#! /usr/bin/env python
#################################################################################
#     File Name           :     readJson.py
#     Created By          :     tiboiser
#     Creation Date       :     [2020-05-21 04:27]
#     Last Modified       :     [2020-06-24 14:57]
#     Description         :     Receive JSON from C#, format it and send it to ROS
#################################################################################

import base64
import rospy
import json
import socket
from subprocess import Popen
from sensor_msgs.msg import Range
from std_msgs.msg import Float64
from std_msgs.msg import Time
from sensor_msgs.msg import CompressedImage
from sensor_msgs.msg import CameraInfo
import subprocess


HOST = "0.0.0.0"
PORT = 55566

# Sensors class (unserialized json class)
class Sensors:
    def __init__(self, json_def):
        self.__dict__ = json.loads(json_def)
        self.sensors = None if 'sensors' not in self.__dict__ else self.__dict__['sensors']

# Convert json function
class JsonConverter:
    sensorSeq = 0
    def __init__(self):
        rospy.init_node('read_json', anonymous=False)
        global pubRange
        global pubSpeed
        global pubWheelSpeed
        global pubCompressedImage

        # begin compatible data from proprietary constructor
        global p_pubCameraInfo
        global p_pubCompressedImage

        # begin mr_delay
        global d_pubTimeSim
        global d_pubTimeReceiptCamera



        p_CAMERA_INFO_PUBLISHER_TOPIC_NAME = '/camera/camera_info'
        p_COMPRESSED_IMAGE_PUBLISHER_TOPIC_NAME = '/camera/image_raw/compressed'
        p_pubCameraInfo = rospy.Publisher(p_CAMERA_INFO_PUBLISHER_TOPIC_NAME, CameraInfo, queue_size=1)
        p_pubCompressedImage = rospy.Publisher(p_COMPRESSED_IMAGE_PUBLISHER_TOPIC_NAME, CompressedImage, queue_size=1)
        # end compatible...


        d_TIME_SIM_PUBLISHER_TOPIC_NAME = '/mr_delay/time_sim'
        d_TIME_RECEIPT_CAMERA_PUBLISHER_TOPIC_NAME = '/mr_delay/time_receipt_camera'
        d_pubTimeSim = rospy.Publisher(d_TIME_SIM_PUBLISHER_TOPIC_NAME, Time, queue_size=1)
        d_pubTimeReceiptCamera = rospy.Publisher(d_TIME_RECEIPT_CAMERA_PUBLISHER_TOPIC_NAME, Time, queue_size=1)


        SPEED_PUBLISHER_TOPIC_NAME = rospy.get_param('/sensor_handler_publisher_topic_name','/mr_sensor_handler/sensor_speed')
        WHEEL_SPEED_PUBLISHER_TOPIC_NAME = rospy.get_param('/sensor_handler_publisher_topic_name','/mr_sensor_handler/sensor_rear_wheel_speed')
        RANGE_PUBLISHER_TOPIC_NAME = rospy.get_param('/sensor_handler_publisher_topic_name','/mr_sensor_handler/sensor_range')
        COMPRESSED_IMAGE_PUBLISHER_TOPIC_NAME = rospy.get_param('/sensor_handler_publisher_topic_name','/mr_sensor_handler/sensor_compressed_image')
        pubSpeed = rospy.Publisher(SPEED_PUBLISHER_TOPIC_NAME, Float64, queue_size=1)
        pubWheelSpeed = rospy.Publisher(WHEEL_SPEED_PUBLISHER_TOPIC_NAME, Float64, queue_size=1)
        pubRange = rospy.Publisher(RANGE_PUBLISHER_TOPIC_NAME, Range, queue_size=1)
        pubCompressedImage = rospy.Publisher(COMPRESSED_IMAGE_PUBLISHER_TOPIC_NAME, CompressedImage, queue_size=1)

    # Convert the json in multiples ros messages
    def jsonToRosMsg(self, json):

        def speedJson(speedJson):
            JsonConverter.sensorSeq += 1
            if len(speedJson["sensorData"]["$values"]) < 1:
                return None
            # sec, nsec = milliToStamp(radarJson["sensorData"]["$values"][-1]["timestamp"])

            speed = Float64()
            speed.data = float(speedJson["sensorData"]["$values"][-1]["selfSpeed"])

            wheelSpeed = Float64()
            wheelSpeed.data = speed.data / 0.35

            pubSpeed.publish(speed)
            pubWheelSpeed.publish(wheelSpeed)


        # Radar conversion
        def radarJson(radarJson):
            JsonConverter.sensorSeq += 1
            if len(radarJson["sensorData"]["$values"]) < 1:
                return None

            sec, nsec = milliToStamp(radarJson["sensorData"]["$values"][-1]["timestamp"])
            r = Range()
            r.header.seq = JsonConverter.sensorSeq
            r.header.stamp = rospy.Time(sec, nsec)
            r.header.frame_id = "/base_link"
            r.radiation_type = 0
            r.field_of_view = 0.0
            r.min_range = 0.0
            r.max_range = float(radarJson["sensorData"]["$values"][-1]["data"]["maxDistance"])
            r.range = float(radarJson["sensorData"]["$values"][-1]["data"]["distance"])
            pubRange.publish(r)

            # msg = "header:\n  seq: " + str(JsonConverter.sensorSeq) + "\n  stamp: {secs: " + str(sec) + ", nsecs: " + str(nsec) + "}\n  frame_id: '/base_link'\nradiation_type: 0\nfield_of_view: 0\nmin_range: 0\nmax_range: " +  str(radarJson["sensorData"]["$values"][-1]["data"]["maxDistance"]) + "\nrange: " + str(radarJson["sensorData"]["$values"][-1]["data"]["distance"])
            msg = None
            return msg

        def cameraJson(cameraJson):
            JsonConverter.sensorSeq += 1
            if len(cameraJson["sensorData"]["$values"]) < 1:
                return None
            sec, nsec = milliToStamp(cameraJson["sensorData"]["$values"][-1]["timestamp"])
            c = CompressedImage()
            c.header.seq = JsonConverter.sensorSeq
            c.header.stamp = rospy.Time(sec, nsec)
            c.header.frame_id = "camera"
            c.format = "png" # can be "bgr8; jpeg compressed bgr8"
            c.data = [ord(elem) for elem in base64.b64decode(cameraJson["sensorData"]["$values"][-1]["data"]["$value"])]
            # c.data.extend(base64.b64decode(cameraJson["sensorData"]["$values"][-1]["data"]["$value"]))


            p_ci = CameraInfo()
            p_ci.header.seq = JsonConverter.sensorSeq
            p_ci.header.stamp = rospy.Time(sec, nsec)
            p_ci.header.frame_id = "camera"
            p_ci.height = 480
            p_ci.width = 640
            p_ci.distortion_model = "plumb_bob"
            p_ci.D = [-0.3544821597318962, 0.09573939430015938, 0.0004901530457421206, 0.0004692845058550044, 0.0]
            p_ci.K = [368.8629923375451, 0.0, 315.8073200419598, 0.0, 369.3810585365014, 211.0329743822299, 0.0, 0.0, 1.0]
            p_ci.R = [1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0]
            p_ci.P = [260.7480773925781, 0.0, 315.8275826189638, 0.0, 0.0, 295.3787231445312, 195.8080259082635, 0.0, 0.0, 0.0, 1.0, 0.0]
            p_ci.binning_x = 0
            p_ci.binning_y = 0
            p_ci.roi.x_offset = 0
            p_ci.roi.y_offset = 0
            p_ci.roi.height = 0
            p_ci.roi.width = 0
            p_ci.roi.do_rectify = False

            d_tsim_data = c.header.stamp

            d_treceiptcamera = rospy.get_rostime()

            p_pubCameraInfo.publish(p_ci)
            p_pubCompressedImage.publish(c)
            pubCompressedImage.publish(c)
            
            d_tsim_data = Time()
            d_tsim_data.data = c.header.stamp
            d_pubTimeSim.publish(d_tsim_data)
            d_treceiptcamera = Time()
            d_treceiptcamera.data = rospy.get_rostime()
            d_pubTimeReceiptCamera.publish(d_treceiptcamera)

            # msg = "header:\n  seq: " + str(JsonConverter.sensorSeq) + "\n  stamp: {secs: " + str(sec) + ", nsecs: " + str(nsec) + "}\n  frame_id: '/base_link'\nformat: 'png'\ndata: " +  str(cameraJson["sensorData"]["$values"][-1]["data"]["$value"])
            msg = None
            return msg


        sensors = Sensors(json)
        for sensor in sensors.sensors["$values"]:
            if (sensor["$type"] == 'SensorRadarExport, Assembly-CSharp'):
                msg = radarJson(sensor)
                if msg is not None:
                    p = Popen(['rostopic', 'pub', '-1', '/mr_sensor_handler/sensor_range', 'sensor_msgs/Range', msg])
                    # p.terminate
            if (sensor["$type"] == 'SensorCameraExport, Assembly-CSharp'):
                msg = cameraJson(sensor)
                if msg is not None:

                    print(len(msg))
                    if (len(msg) > 125000):
                        print("Image heavy")
                    p = Popen(['rostopic', 'pub', '-1', '/mr_sensor_handler/sensor_compressed_image', 'sensor_msgs/CompressedImage', msg])
                    # subprocess.check_call(['rostopic', 'pub', '/mr_sensor_handler/sensor_compressed_image', 'sensor_msgs/CompressedImage', msg], shell=True)
            if (sensor["$type"] == 'SensorSpeedExport, Assembly-CSharp'):
                msg = speedJson(sensor)
                if msg is not None:
                    pass

                    # p.terminate
            if (sensor["$type"] == 'SensorTruthExport, Assembly-CSharp'):
                pass
            if (sensor["$type"] == 'SensorLidarExport, Assembly-CSharp'):
                pass




#### Networking section ####
# Handler when something is received
def listenerHandler(reader, writer):
    while (True):
        data = reader.readline()
        if data is not None:
            jc = JsonConverter()
            jc.jsonToRosMsg(data)

def listener():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.bind((HOST, PORT))
    s.listen(1)
    print("Waiting for connection on " + HOST + ", " + str(PORT))
    connected, addr = s.accept()
    print("Accepted connection")
    socketFile = connected.makefile()
    jc = JsonConverter()
    while (1):
        data = socketFile.readline()
        if data is not None:
            jc.jsonToRosMsg(data)
    # server = await asyncio.start_server(listenerHandler, HOST, PORT, limit=1024*1024*10) # Limit json size at 10mo
    # addr = server.sockets[0].getsockname()
    # print(f'Serving on {addr}')
    #
    # async with server:
    #     await server.serve_forever()


### Utility functions ###
# Convert from milli since epoch to rounded nanoseconds and seconds in ROS format
def milliToStamp(time):
    return int(time / 1000), int((time % 1000) * 1e6)



if __name__ == '__main__':
    listener()
    try:
        pass
    except rospy.ROSInterruptException:
        pass
    except:
        pass
