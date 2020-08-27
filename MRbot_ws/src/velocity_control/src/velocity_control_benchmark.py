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
turning_offset = 15
MAX_SPEED = 100

emergency_stop_status = 0
'''
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
'''
def callback_rear_wheel():
    if(emergency_stop_status):
        emergency_stop()
        return
    bw_speed = 0.2 * MAX_SPEED
    if(bw_speed > 0):
        bw.forward()
    else:
        bw.backward()
    bw.speed = min(abs(int(bw_speed)),MAX_SPEED)


def callback_front_wheel():
    if(emergency_stop_status):
        emergency_stop()
        return
    fw.turn(int(turning_offset + 90 - fw.turning_max * 0))

def emergency_stop():
    bw.stop()
    fw.turn(90+ turning_offset)

if __name__ == '__main__':
    try:
        #rospy.init_node('velocity_control', anonymous=False)
        #rospy.Subscriber("rear_wheel_command", Float64, callback_rear_wheel)
        #rospy.Subscriber("front_wheel_command", Float64, callback_front_wheel)
        #s = rospy.Service('emergency_stop', SetBool, handle_service)
        emergency_stop()
	#rospy.spin()
	old_time = time.time()
    except KeyboardInterrupt:
        destroy()
    while 1:
	now = time.time()
	dt = now - old_time
	print("dt = " + str(dt))
	callback_rear_wheel()
	callback_front_wheel()
	old_time = now
