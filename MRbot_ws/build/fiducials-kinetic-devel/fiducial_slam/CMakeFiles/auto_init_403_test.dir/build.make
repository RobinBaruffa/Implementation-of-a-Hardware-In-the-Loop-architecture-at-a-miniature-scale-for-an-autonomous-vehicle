# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.5

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /usr/bin/cmake

# The command to remove a file.
RM = /usr/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /home/user/MRbot_ws/src

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /home/user/MRbot_ws/build

# Include any dependencies generated for this target.
include fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/depend.make

# Include the progress variables for this target.
include fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/progress.make

# Include the compile flags for this target's objects.
include fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/flags.make

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/flags.make
fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o: /home/user/MRbot_ws/src/fiducials-kinetic-devel/fiducial_slam/test/auto_init_403_test.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/home/user/MRbot_ws/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o"
	cd /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam && /usr/bin/c++   $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o -c /home/user/MRbot_ws/src/fiducials-kinetic-devel/fiducial_slam/test/auto_init_403_test.cpp

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.i"
	cd /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam && /usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /home/user/MRbot_ws/src/fiducials-kinetic-devel/fiducial_slam/test/auto_init_403_test.cpp > CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.i

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.s"
	cd /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam && /usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /home/user/MRbot_ws/src/fiducials-kinetic-devel/fiducial_slam/test/auto_init_403_test.cpp -o CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.s

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.requires:

.PHONY : fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.requires

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.provides: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.requires
	$(MAKE) -f fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/build.make fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.provides.build
.PHONY : fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.provides

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.provides.build: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o


# Object files for target auto_init_403_test
auto_init_403_test_OBJECTS = \
"CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o"

# External object files for target auto_init_403_test
auto_init_403_test_EXTERNAL_OBJECTS =

/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/build.make
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: gtest/gtest/libgtest.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libtf2_ros.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libactionlib.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libtf2.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libimage_transport.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libmessage_filters.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libtinyxml2.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libclass_loader.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/libPocoFoundation.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libdl.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libroscpp.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_signals.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libxmlrpcpp.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libroslib.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/librospack.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libpython2.7.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_filesystem.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_program_options.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libtinyxml.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libcv_bridge.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_core3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_imgproc3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_imgcodecs3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/librosconsole.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/librosconsole_log4cxx.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/librosconsole_backend_interface.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/liblog4cxx.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_regex.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libroscpp_serialization.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/librostime.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/libcpp_common.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_system.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_thread.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_chrono.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_date_time.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libboost_atomic.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libpthread.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /usr/lib/x86_64-linux-gnu/libconsole_bridge.so
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_stitching3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_superres3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_videostab3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_aruco3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_bgsegm3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_bioinspired3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_ccalib3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_cvv3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_dpm3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_face3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_fuzzy3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_hdf3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_img_hash3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_line_descriptor3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_optflow3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_reg3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_rgbd3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_saliency3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_stereo3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_structured_light3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_surface_matching3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_tracking3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_xfeatures2d3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_ximgproc3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_xobjdetect3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_xphoto3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_shape3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_photo3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_datasets3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_plot3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_text3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_dnn3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_ml3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_video3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_calib3d3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_features2d3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_highgui3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_videoio3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_viz3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_phase_unwrapping3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_flann3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_imgcodecs3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_objdetect3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_imgproc3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: /opt/ros/kinetic/lib/x86_64-linux-gnu/libopencv_core3.so.3.3.1
/home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/home/user/MRbot_ws/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Linking CXX executable /home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test"
	cd /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/auto_init_403_test.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/build: /home/user/MRbot_ws/devel/lib/fiducial_slam/auto_init_403_test

.PHONY : fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/build

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/requires: fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/test/auto_init_403_test.cpp.o.requires

.PHONY : fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/requires

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/clean:
	cd /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam && $(CMAKE_COMMAND) -P CMakeFiles/auto_init_403_test.dir/cmake_clean.cmake
.PHONY : fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/clean

fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/depend:
	cd /home/user/MRbot_ws/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/user/MRbot_ws/src /home/user/MRbot_ws/src/fiducials-kinetic-devel/fiducial_slam /home/user/MRbot_ws/build /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam /home/user/MRbot_ws/build/fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : fiducials-kinetic-devel/fiducial_slam/CMakeFiles/auto_init_403_test.dir/depend

