#!/usr/bin/env python
#import roslib

import rospy
import math
import tf

from std_msgs.msg import Float64


if __name__ == '__main__':
    rospy.init_node('angle_calculation_node')
    listener = tf.TransformListener()

    pub = rospy.Publisher('angle', Float64,queue_size=1)

    rate = rospy.Rate(30.0)
    while not rospy.is_shutdown():
        try:
            (trans,rot) = listener.lookupTransform('/camera', '/fiducial_42', rospy.Time(0))
        except (tf.LookupException, tf.ConnectivityException, tf.ExtrapolationException):
            continue
	#trans is the vector from /camera to /fiducial_42
	#trans is the quaternion of the angle btw /camera and /fiducial_42

    	angle = Float64()
    	angle.data = math.atan2(trans[0],trans[2])

    	pub.publish(angle)

    	rate.sleep()
