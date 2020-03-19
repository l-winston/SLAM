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
| <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?7a5f3aee72" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/>     | is the true value |
| <img alt="$z_n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a5877527319d66afa46d823ccc099bf4.svg?69b6f28e86" align="middle" width="15.77067689999999pt" height="14.15524440000002pt"/>   | is the measurement value of the weight at time <img alt="$n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/55a049b8f161ae7cfeb0197d75aff967.svg?a7195182f" align="middle" width="9.86687624999999pt" height="14.15524440000002pt"/> |
| <img alt="$\hat{x}_{n, n}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/26bae1845d0ce0ef4bfa9641516d562a.svg?67f6e28d7" align="middle" width="29.55116669999999pt" height="22.831056599999986pt"/> | is the estimate of <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?9e0864f4d8" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/> at time <img alt="$n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/55a049b8f161ae7cfeb0197d75aff967.svg?f1515c14" align="middle" width="9.86687624999999pt" height="14.15524440000002pt"/> (the estimate is made after taking the measurement <img alt="$z_n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a5877527319d66afa46d823ccc099bf4.svg?ab8f47cee3" align="middle" width="15.77067689999999pt" height="14.15524440000002pt"/>)|
| <img alt="$\hat{x}_{n, n-1}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/d49a6d1c13901555bdc3538e0aa68892.svg?c66bc0f557" align="middle" width="46.37773469999998pt" height="22.831056599999986pt"/>  | is the previous estimate of <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?b48141112e" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/> that was made at time <img alt="$n-1$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/efcf8d472ecdd2ea56d727b5746100e3.svg?9e56012e2e" align="middle" width="38.17727759999999pt" height="21.18721440000001pt"/> (the estimate was made after taking the measurement <img alt="$z_{n-1}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/9431fe18d89f4aed4379a6e4d6339c27.svg?168499afe" align="middle" width="32.59724489999999pt" height="14.15524440000002pt"/>)   |
| <img alt="$\hat{x}_{n+1, n}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/e7e21ca4db21279d24f0c7f0b1e055ea.svg?20265819a5" align="middle" width="46.19508794999999pt" height="22.831056599999986pt"/>  | is the estimate of the future state (<img alt="$n+1$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/3f18d8f60c110e865571bba5ba67dcc6.svg?7afb433ec" align="middle" width="38.17727759999999pt" height="21.18721440000001pt"/>) of <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?3de2a0491f" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/>. The estimate is made at the time <img alt="$n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/55a049b8f161ae7cfeb0197d75aff967.svg?1a6fa13d05" align="middle" width="9.86687624999999pt" height="14.15524440000002pt"/>, right after the measurement <img alt="$z_n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a5877527319d66afa46d823ccc099bf4.svg?3c31c277" align="middle" width="15.77067689999999pt" height="14.15524440000002pt"/>. In other words, <img alt="$x_{n, n+1}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a41bbe848dcc636e1a54404dafd313a1.svg?b419c16fc0" align="middle" width="46.19508794999999pt" height="14.15524440000002pt"/> is a predicted state  |



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
