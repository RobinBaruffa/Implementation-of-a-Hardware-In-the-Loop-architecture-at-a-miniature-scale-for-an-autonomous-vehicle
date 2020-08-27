#!/usr/bin/env python

import rospy
import time
import picar

from std_msgs.msg import Float64
from std_srvs.srv import SetBool,SetBoolResponse

from picar import front_wheels
from picar import back_wheels

picar.setup()

fw = front_wheels.Front_Wheels(db='config')
bw = back_wheels.Back_Wheels(db='config')
fw.ready()
bw.ready()

fw.turning_max = 40
turning_offset = 9 #10 va a droite
MAX_SPEED = 100
simMaxRPM = 400
emergency_stop_status = 0
rear_wheel_dead_zone = 0.1

def handle_service(req):
    response = SetBoolResponse()
    if(req.data == 1):
        emergency_stop_status = 1
        rospy.loginfo("EMERGENCY STOP TRIGGERED ! ")
        response.success = 1
        response.message = "EMERGENCY STOP TRIGGERED !"
	emergency_stop()

    else:
        emergency_stop_status = 0
        rospy.loginfo("Emergency stop released, the car is now operational")
        response.success = 1
        response.message = "Emergency stop released, the car is now operational"

    return response


def callback_rear_wheel(data):
    if(emergency_stop_status):
        emergency_stop()
        return
    if(data.data > 0):
        bw_speed = rear_wheel_dead_zone * MAX_SPEED+ mapAndClamp(data.data,-simMaxRPM,simMaxRPM,-1 + rear_wheel_dead_zone,1 - rear_wheel_dead_zone) * MAX_SPEED
    else:
        bw_speed = - rear_wheel_dead_zone * MAX_SPEED + mapAndClamp(data.data,-simMaxRPM,simMaxRPM,-1 + rear_wheel_dead_zone,1 - rear_wheel_dead_zone) * MAX_SPEED
    if(bw_speed > 0):
        bw.forward()
    else:
        bw.backward()
    bw.speed = min(abs(int(bw_speed)),MAX_SPEED)


def callback_front_wheel(data):
    if(emergency_stop_status):
        emergency_stop()
        return
    front_wheel_command = mapAndClamp(data.data,-0.32,0.32,-fw.turning_max,fw.turning_max)
    #front_wheel_command = mapAndClamp(data.data,-0.32,0.32,-fw.turning_max,fw.turning_max)
    fw.turn(int(turning_offset + 90 + front_wheel_command))

def emergency_stop():
    bw.stop()
    fw.turn(90+ turning_offset)

def mapAndClamp(x, in_min, in_max, out_min, out_max):
    mapped =  float((x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min)
    return max(min(mapped, out_max), out_min)


if __name__ == '__main__':
    try:
        rospy.init_node('velocity_control', anonymous=False)
        rospy.Subscriber("mr_sensor_handler/sensor_speed", Float64, callback_rear_wheel)
        rospy.Subscriber("front_wheel_command", Float64, callback_front_wheel)
        rospy.Service('emergency_stop', SetBool, handle_service)
        emergency_stop()
	#rospy.spin()
    except KeyboardInterrupt:
        destroy()
    while not rospy.is_shutdown():
	rospy.spin()
    emergency_stop()
