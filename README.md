# SLAM
SLAM (Simultaneous Localization And Mapping) is the process of creating a map of an environment while tracking the agent's location within it.

>SLAM is a framework for temporal modeling of states that is commonly used in autonomous navigation. It is heavily based on principles of probability, making inferences on posterior and prior probability distributions of states and measurements and the relationship between the two. [1]

## Step 1: Prediction
We estimate our current position with a motion model by using previous positions, the current control input, and noise.

## Step 2: Measurement
Then, in the measurement step, we correct our prediction by using data from our sensors.

**Proprioceptive sensors** measure internal values - like acceleration, motor speeds, battery voltage, etc. For this project, I'll be simulating an inertial measurement unit by taking discrete measurement of artificially noisy (possibly systematic noise) linear and angular acceleration data.

**Exteroceptive sensors** collect data about the robot's environment, like distances to objects, light, sound, etc. For this project, I'll be simulating a collection of distance sensors by casting rays out from a "sensor" and recording the distances they travel.

## Kalman Filters

Kalman Filters are a way for us to estimate the (unknown) actual state of an system (our agent's position and orientation) through the collection of noisy data.

We can give more weight to data that we know is more accurate by increasing the Kalman Gain associated with that measurement.

We take the state of a system and use a set of equations (ex. laws of physics) to extrapolate a predicted future state.

<p align="center">
<img width=40% alt="Notation" src="/images/Notation.png">
</p>


<p align="center"> <b> The Five Kalman Filter Equations: </b> </p>

<p align="center">
State Update Equation:
<br>
<img style="width:50%" alt="State Update Equation" src="/images/StateUpdateEquation.png">
<br>
State Update Equation Description
</p>

<p align="center">
State Extrapolation Equations:
<br>
<img style="width:50%" alt="State Extrapolation Equation" src="/images/StateExtrapolationEquation1.png">
<br>
State Extrapolation Equation 1 Description

<img style="width:50%" alt="State Extrapolation Equation" src="/images/StateExtrapolationEquation2.png">
<br>
State Extrapolation Equation 2 Description
</p>

<p align="center">
Kalman Gain Equation:
<br>
<img style="width:50%" alt="Kalman Gain Equation" src="/images/KalmanGainEquation.png">
<br>
Kalman Gain Equation Description
</p>

<p align="center">
Covariance Update Equation
<br>
<img style="width:50%" alt="Covariance Update Equation" src="/images/CovarianceUpdateEquation.png">
</p>

<p align="center">
Covariance Extrapolation Equation
<br>
<img style="width:50%" alt="Covariance Extrapolation Equation" src="/images/CovarianceExtrapolationEquation.png">
</p>

Next, we take a measurement, tweak our estimation of the state, and then once again predict the future state.

<p align="center">
<img alt="Estimation Algorithm Flowchart" src="https://www.kalmanfilter.net/img/AlphaBeta/ex2_estimationAlgorithm.png">
</p>

# Sources & Resources
1. [How does Autonomous Driving Work? An Intro into SLAM](https://towardsdatascience.com/slam-intro-fd833ef29e4e)
2. [Simultaneous Localization and Mapping](https://en.wikipedia.org/wiki/Simultaneous_localization_and_mapping)
3. [MatLab: Understanding Kalman Filters](https://www.youtube.com/watch?v=mwn8xhgNpFY&list=PLn8PRpmsu08pzi6EMiYnR-076Mh-q3tWr)
4. https://www.kalmanfilter.net/default.aspx
