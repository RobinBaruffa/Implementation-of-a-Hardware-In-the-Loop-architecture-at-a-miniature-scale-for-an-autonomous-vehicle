#!/usr/bin/env python

import rospy
import time

from std_msgs.msg import Float64

previousSpeed = Float64()
previousSpeed.data = 0.0
previousMeasurement = 0.0
previousTime = time.time()
firstLoop = True

def callback_rear_wheel(data):
    global previousSpeed
    global previousTime
    global firstLoop
    global previousMeasurement

    if firstLoop:
        previousTime = time.time()
        previousMeasurement = data.data
        firstLoop = False
        return
    now = time.time()
    dt = now - previousTime
    print("\ndt : " + str(dt))
    print("\nintermediaire : " + str(previousSpeed.data + previousMeasurement * dt + 0.5*dt*(data.data-previousMeasurement)))
    newSpeed = Float64()
    newSpeed.data = previousSpeed.data + previousMeasurement * dt + 0.5*dt*(data.data-previousMeasurement)
    previousTime = now
    previousMeasurement = data.data
    previousSpeed.data = newSpeed.data
    p.publish(newSpeed)


if __name__ == '__main__':
        rospy.init_node('robot_rear_wheel_integrator', anonymous=False)
        rospy.Subscriber("rear_wheel_command_acceleration", Float64, callback_rear_wheel)
        p = rospy.Publisher('rear_wheel_robot_command_velocity', Float64, queue_size=1)
        while not rospy.is_shutdown():
    	       rospy.spin()
