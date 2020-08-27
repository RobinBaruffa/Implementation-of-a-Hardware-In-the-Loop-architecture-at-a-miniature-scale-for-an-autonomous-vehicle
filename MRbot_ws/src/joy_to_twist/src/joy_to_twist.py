#!/usr/bin/env python
import rospy

from sensor_msgs.msg import Joy
from geometry_msgs.msg import Twist


def compute(msg):
    twist = Twist()
    twist.linear.y = msg.axes[1]
    twist.angular.z = msg.axes[2]
    #result = Vector3()
    #result . 
    pub.publish(twist)
    return 0

def main():
    rospy.init_node('joy_to_twist_node')
    global pub
    pub= rospy.Publisher('/velocity_command', Twist, queue_size=1)
    rospy.Subscriber("/joy",Joy ,compute)

    rospy.spin()

if __name__ == "__main__":
    try:
        main()
    except rospy.ROSInterruptException:
        pass
