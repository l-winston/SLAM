<p align="center"><img src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/32737e0a8d5a4cf32ba3ab1b74902ab7.svg?e84b7aa2b4&invert_in_darkmode" align=middle width=127.9847844pt height=39.452455349999994pt/></p>


It is well known that if <img src="https://rawgit.com/l-winston/SLAM/master/svgs/15b9e78f3a7cb11ea59b95c9553fb928.svg?8490e2b618&invert_in_darkmode" align=middle width=119.34141284999998pt height=26.76175259999998pt/>, then <img src="https://rawgit.com/l-winston/SLAM/master/svgs/2b1f70f6a49aea806b0a5f021e843447.svg?15f490364e&invert_in_darkmode" align=middle width=112.44128444999998pt height=33.20539200000001pt/>.

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

| Notation | Meaning |
| -------- | ------- |
| ![x]     | is the true value |
| ![z_n]   | is the measurement value of the weight at time ![n] |
| ![hat{x}_{n, n}] | is the estimate of ![x] at time ![n] (the estimate is made after taking the measurement ![z_n])|
| ![hat{x}_{n, n - 1}]  | is the previous estimate of ![x] that was made at time ![n-1] (the estimate was made after taking the measurement ![z_n-1])   |
| ![hat{x}_{n + 1, n}]  | is the estimate of the future state (![n+1]) of ![x]. The estimate is made at the time ![n], right after the measurement ![z_n]. In other words, ![x_{n, n+1}] is a predicted state  |



[n-1]: https://latex.codecogs.com/svg.latex?\inline&space;\LARGE&space;n-1
[n+1]: https://latex.codecogs.com/svg.latex?\inline&space;\LARGE&space;n+1
[n]: https://latex.codecogs.com/svg.latex?\inline&space;\LARGE&space;n
[x]: https://latex.codecogs.com/svg.latex?\inline&space;\LARGE&space;x_
[z_n]: https://latex.codecogs.com/svg.latex?\inline&space;\LARGE&space;z_n
[z_n-1]: https://latex.codecogs.com/svg.latex?\inline&space;\LARGE&space;z_n-1
[hat{x}_{n, n}]: https://latex.codecogs.com/svg.latex?\inline&space;\dpi{200}\large&space;\hat{x}_{n,&space;n}
[hat{x}_{n, n - 1}]: https://latex.codecogs.com/svg.latex?\inline&space;\dpi{300}\large&space;\hat{x}_{n,&space;n-1}
[hat{x}_{n + 1, n}]: https://latex.codecogs.com/svg.latex?\inline&space;\dpi{300}\large&space;\hat{x}_{n+1,&space;n}
[x_{n, n+1}]: https://latex.codecogs.com/svg.latex?\inline&space;\dpi{300}\large&space;x_{n,&space;n+1}

<p align="center"> <b> The Five Kalman Filter Equations: </b> </p>



<p align="center">
State Update Equation:
<br>

<img src="https://latex.codecogs.com/gif.latex?\inline&space;\dpi{300}&space;\huge&space;\hat{x}_{n,&space;n}&space;=&space;\hat{x}_{n,&space;n-1}&space;&plus;&space;K_n&space;(z_n&space;-&space;\hat{x}_{n,&space;n-1})"/>

<br>
State Update Equation Description
</p>




<p align="center">
State Extrapolation Equations:
<br>
<img src="images/StateExtrapolationEquation1.png">
<br>
<img src="images/StateExtrapolationEquation2.png">
<br>
State Extrapolation Equation 1 Description
<br>s
State Extrapolation Equation 2 Description
</p>



<p align="center">
Kalman Gain Equation:
<br>
<img src="images/KalmanGainEquation.png">
<br>
Kalman Gain Equation Description
</p>

<p align="center">
Covariance Update Equation
<br>
<img src="images/CovarianceUpdateEquation.png">
<br>
Covariance Update Equation Description
</p>

<p align="center">
Covariance Extrapolation Equation
<br>
<img src="images/CovarianceExtrapolationEquation.png">
<br>
Covariance Extrapolation Equation  Description
</p>

Next, we take a measurement, tweak our estimation of the state, and then once again predict the future state.

<p align="center">
<img width="100% "alt="Estimation Algorithm Flowchart" src="https://www.kalmanfilter.net/img/AlphaBeta/ex2_estimationAlgorithm.png">
</p>

# Sources & Resources
1. [How does Autonomous Driving Work? An Intro into SLAM](https://towardsdatascience.com/slam-intro-fd833ef29e4e)
2. [Simultaneous Localization and Mapping](https://en.wikipedia.org/wiki/Simultaneous_localization_and_mapping)
3. [MatLab: Understanding Kalman Filters](https://www.youtube.com/watch?v=mwn8xhgNpFY&list=PLn8PRpmsu08pzi6EMiYnR-076Mh-q3tWr)
4. https://www.kalmanfilter.net/default.aspx
