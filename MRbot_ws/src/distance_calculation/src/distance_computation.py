#!/usr/bin/env python
#import roslib

import rospy
import math
#import tf

from std_msgs.msg import Float64
from std_msgs.msg import Time

from tf2_msgs.msg import TFMessage

def callback(data):
    trans = data.transforms[0].transform.translation
    distance = Float64()
    distance.data = math.sqrt(trans.x ** 2 + trans.z ** 2)
    global turtle_vel
    turtle_vel.publish(distance)
    reception_time = Time()
    reception_time.data = rospy.get_rostime()
    global time_turtle_vel
    time_turtle_vel.publish(reception_time)
    return

if __name__ == '__main__':
    rospy.init_node('distance_calculation_node')
    global turtle_vel
    turtle_vel = rospy.Publisher('distance', Float64,queue_size=1)
    global time_turtle_vel
    time_turtle_vel = rospy.Publisher('/mr_delay/time_distance', Time,queue_size=1)
    rospy.Subscriber("tf", TFMessage, callback)

    while not rospy.is_shutdown():
        rospy.spin()
















'''
if __name__ == '__main__':
    rospy.init_node('distance_calculation_node')
    listener = tf.TransformListener()

    turtle_vel = rospy.Publisher('distance', Float64,queue_size=1)
    time_turtle_vel = rospy.Publisher('/mr_delay/time_distance', Time,queue_size=1)

    #rate = rospy.Rate(30.0)
    while not rospy.is_shutdown():
        try:
            (trans,rot) = listener.lookupTransform('/camera', '/fiducial_42', rospy.Time(0))
        except (tf.LookupException, tf.ConnectivityException, tf.ExtrapolationException):
            continue
    	#trans is the vector from /camera to /fiducial_42
    	#trans is the quaternion of the angle btw /camera and /fiducial_42

        distance = Float64()
        distance.data = math.sqrt(trans[0] ** 2 + trans[2] ** 2)
        turtle_vel.publish(distance)
        reception_time = Time()
        reception_time.data = rospy.get_rostime()
        time_turtle_vel.publish(reception_time)
        rate.sleep()
'''
