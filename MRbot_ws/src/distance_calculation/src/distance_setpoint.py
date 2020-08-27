#!/usr/bin/env python

import rospy

from distance_calculation.cfg import DynamicParamsConfig
from std_msgs.msg import Float64

# Give ourselves the ability to run a dynamic reconfigure server.
from dynamic_reconfigure.server import Server as DynamicReconfigureServer



class DistanceSetpoint(object):
    """Node example class."""

    def __init__(self):
        """Read in parameters."""
        # Get the private namespace parameters from the parameter server:
        # set from either command line or launch file.

	#For unknwown reasons, if set to 5, the PID controller freaks out
	rate = 7

        # Initialize enable variable so it can be used in dynamic reconfigure
        # callback upon startup.
        # Create a dynamic reconfigure server.
        self.server = DynamicReconfigureServer(DynamicParamsConfig, self.reconfigure_cb)
        # Create a publisher for our custom message.
        self.pub = rospy.Publisher("distance_setpoint", Float64, queue_size=10)

        self.message = Float64()

        # Create a timer to go to a callback at a specified interval.
        rospy.Timer(rospy.Duration(1.0 / rate), self.timer_cb)


    def timer_cb(self, _event):
        """Call at a specified interval to publish message."""

        # Set the message type to publish as our custom message.
        msg = Float64()
        # Assign message fields to values from the parameter server.
        msg.data = rospy.get_param("~distance_setpoint", self.message)
        self.message = msg
        # Fill in custom message variables with values updated from dynamic
        # reconfigure server.
        #self.message.data = msg.data

        # Publish our custom message.
        self.pub.publish(self.message)

    def reconfigure_cb(self, config, dummy):
        """Create a callback function for the dynamic reconfigure server."""
        # Fill in local variables with values received from dynamic reconfigure
        # clients (typically the GUI).
        self.message = config["distance_setpoint"]
        # Return the new variables.
        return config


# Main function.
if __name__ == "__main__":
    # Initialize the node and name it.
    rospy.init_node("distance_setpoint")
    # Go to class functions that do all the heavy lifting.
    try:
        DistanceSetpoint()
    except rospy.ROSInterruptException:
        pass
    # Allow ROS to go to all callbacks.
    rospy.spin()
