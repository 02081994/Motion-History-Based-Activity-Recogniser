# Motion-History-Based-Activity-Recogniser

### Problem Statement

To write a program which captures the video feed using webcam, generates a motion history image and then uses it for activity recognition.

### Assumptions

1. It is a cooperative recognition technique.
2. The lighting conditions are proper.
3. There are no other disturbances or movements in the video feed.
4. Only one person is standing in front of the camera.

### Data Description

The data is video feed taken from the webcam of the computer wherein different people are doing same set of activities like moving right hand, moving left hand, both hands or bending. 

Each data point consists has 10 attributes collected for 80 frames. 5 such data points have been collected for each class.

### Preprocessing Done

We have already trained the algorithm on 20 different video feeds. People of different heights and body figure have been included in the data set to make it more robust. Variant light conditions were also included to make it more reliable.

### Classification Algorithm

The project can be divided into four stages:

1. Taking input from Webcam
2. Creating motion history image from the video feed
3. Apply 2D SVD on the motion history image
4. Nearest Neighbor for classification.

### Result 

After taking 50 video feeds as training data set we ran the algorithm on 50 people and it gave correct results for 46 of them. Accuracy being 92.00%.

### Analysis

The 2D-SVD and nearest neighbor approach worked quite well if proper care is given to light conditions and the pose of person in consideration.

##### Following are the pros and cons of the approach:

###### Pros:
Use of 2D SVD resulted in a comparatively low dimension which increased the speed of algorithm although reducing the accuracy a bit.
 Even the nearest neighbor algorithm served the purpose quite well.

###### Cons:
Light conditions and pose of the person still affect the accuracy of the algorithm.

### Some other observations are as follows

The values of r and s for the algorithm were obtained using the scree test and it was found that 5 is a good value for both r and s.

The algorithm was made to run on different values of k (in knn). The results were significantly better for k=3 than for k=1. The number of windows or tiles in the motion history image which serve as the feature vector could be increased further from 10 to 20. We tried on 5, 10 and 20. Although the accuracy would increase on 20 but it caused a significant lag which is not desired from a practical point of view. So a set of 10 windows have been chosen to serve as feature vector.


