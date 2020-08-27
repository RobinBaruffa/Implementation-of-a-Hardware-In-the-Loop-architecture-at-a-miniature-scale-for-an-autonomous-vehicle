#!/usr/bin/env python
import sys
sys.path.append("/home/ubuntu/SunFounder_PiCar-S/example")
import rospy
import picar
from sensor_msgs.msg import Range

from SunFounder_Ultrasonic_Avoidance import Ultrasonic_Avoidance
import time

UA = Ultrasonic_Avoidance.Ultrasonic_Avoidance(20)
threshold = 10


if __name__=='__main__':

	rospy.init_node('ultrasonic_sensor_node', anonymous=False)
	pub = rospy.Publisher('ultrasonic_distance', Range,queue_size=1)
	rate = rospy.Rate(30.0)
	message = Range()

 	message.radiation_type = 1
	message.field_of_view = 0.523599
	message.min_range = 0.01
	message.max_range = 3
	message.header.frame_id = "camera"
	i = 0
    	while not rospy.is_shutdown():
		distance = distance = UA.get_distance() * 0.01
		if(distance !=-1):
			message.header.seq = i
			message.header.stamp = rospy.Time.now()
		    	message.range = distance
		    	pub.publish(message)
			i = i+1
		    	rate.sleep()
