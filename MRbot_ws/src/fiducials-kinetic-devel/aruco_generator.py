#!/usr/bin/env python2

import cv2 as cv
import numpy as np

# Load the predefined dictionary
dictionary = cv.aruco.Dictionary_get(cv.aruco.DICT_5X5_1000)

# Generate the marker
markerImage = np.zeros((200, 200), dtype=np.uint8)
markerImage = cv.aruco.drawMarker(dictionary, 42, 200, markerImage, 1);

cv.imwrite("marker33.png", markerImage);

