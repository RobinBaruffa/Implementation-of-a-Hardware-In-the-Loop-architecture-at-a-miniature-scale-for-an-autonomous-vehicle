#!/usr/bin/env python
#Control_sim is a bridge from ROS to Unity, sending rear and front wheel commands
import rospy
import time

import socket
from std_msgs.msg import Float64

HOST = '192.168.1.40'  #When running on raspberry
#HOST = '127.0.0.1'      #When running on local
PORT = 55567

front_wheel_command = 0.0

def callback_rear_wheel(data):
    global front_wheel_command
    msg = '{"h":' + str(front_wheel_command) + ',"v":' + str(data.data) + ',"hb":0.0}'
    print("\n message :     " + msg)
    global s
    s.sendto(msg, (HOST, PORT))

def callback_front_wheel(data):
    global front_wheel_command
    front_wheel_command = data.data


if __name__ == '__main__':

    rospy.init_node('control_sim', anonymous=False)
    rospy.Subscriber("rear_wheel_command_acceleration", Float64, callback_rear_wheel)
    rospy.Subscriber("front_wheel_command", Float64, callback_front_wheel)
    global s
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    while not rospy.is_shutdown():
	       rospy.spin()
