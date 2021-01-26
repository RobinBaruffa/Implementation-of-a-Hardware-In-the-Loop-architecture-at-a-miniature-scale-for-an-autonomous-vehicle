# Implementation-of-a-Hardware-In-the-Loop-architecture-at-a-miniature-scale-for-an-autonomous-vehicle

This repository is dedicatd to a project realized in by Robin Baruffa and Jacques Pereira during the Spring semester of 2020 in partnership with the Université de Technologie de Belfort-Montbéliard (UTBM) and the CIAD Laboratory.
A 4 wheeled miniature car was built, based on a SunFounder kit, in order to experiment with the possibility of using a miniature robotic platform in order to prototype Hardware-In-the-Loop (HIL) architectures. This approach has numerous advantages, such as being able to benchmark various architectures and control algotithms prior to having access to real (and often expensive) hardware.

#  The academic paper

The project served as a basis for writing and publishing a paper in SIMUL 2020 : The Twelfth International Conference on Advances in System Simulation. [The paper is freely available here.](https://www.researchgate.net/publication/345008126_Mixed_Reality_Autonomous_Vehicle_Simulation_Implementation_of_a_Hardware-In-the-Loop_Architecture_at_a_Miniature_Scale)

<p align="center">
    <img src="https://raw.githubusercontent.com/RobinBaruffa/Implementation-of-a-Hardware-In-the-Loop-architecture-at-a-miniature-scale-for-an-autonomous-vehicle/master/Documentation_and_misc/articleimg.png" height="459" width="357">
</p>

# Overview of the miniature car

The car is controlled by a Raspberry PI4 running Ubuntu 16.04 and the Robot Operating System (ROS) middleware. It can be controlled manually with a keyboard, a custom made GUI or a PS3 controller. It has a camera and an ultrasonic sensor. It is capable of using the camera to detect an aruco marker at the back of another robot, and can follow it while maintaining a dynamically modifiable safe following distance. The ultrasonic sensor is used in case of emergency if the camera readings are unavailable and can engage an emergency braking. 


Miniature car following a wheeled robot with an Aruco marker             |  Camera data overlaid with the estimated Aruco marker pose the PID control over the following distance
:-------------------------:|:-------------------------:
![](https://raw.githubusercontent.com/RobinBaruffa/Implementation-of-a-Hardware-In-the-Loop-architecture-at-a-miniature-scale-for-an-autonomous-vehicle/master/Documentation_and_misc/omniROS_and_robot.JPG)  |  ![](https://raw.githubusercontent.com/RobinBaruffa/Implementation-of-a-Hardware-In-the-Loop-architecture-at-a-miniature-scale-for-an-autonomous-vehicle/master/Documentation_and_misc/rviz.png)


# Hardware-In-the-Loop (HIL) capabilities

A 3D model of a car was modeled using Unity and a script was written to make it capable of executing commands coming from the ROS network.The camera feed from the simulated car in Unity was forwarded to the ROS network. This allows for a completly interchangeable behaviour between the real miniature car and its simulated counterpart.


<p align="center">
    <img src="https://raw.githubusercontent.com/RobinBaruffa/Implementation-of-a-Hardware-In-the-Loop-architecture-at-a-miniature-scale-for-an-autonomous-vehicle/master/Documentation_and_misc/Merged_omniROS_and_robot.JPG" height="430" width="574">
</p>
