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
| <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?b24e2366c3" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/>     | is the true value |
| <img alt="$z_n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a5877527319d66afa46d823ccc099bf4.svg?9446fb18e0" align="middle" width="15.77067689999999pt" height="14.15524440000002pt"/>   | is the measurement value of the weight at time <img alt="$n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/55a049b8f161ae7cfeb0197d75aff967.svg?a01345423b" align="middle" width="9.86687624999999pt" height="14.15524440000002pt"/> |
| <img alt="$\hat{x}_{n, n}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/26bae1845d0ce0ef4bfa9641516d562a.svg?ae75b2752d" align="middle" width="29.55116669999999pt" height="22.831056599999986pt"/> | is the estimate of <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?55db951dff" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/> at time <img alt="$n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/55a049b8f161ae7cfeb0197d75aff967.svg?f61ab01f" align="middle" width="9.86687624999999pt" height="14.15524440000002pt"/> (the estimate is made after taking the measurement <img alt="$z_n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a5877527319d66afa46d823ccc099bf4.svg?4fb493160" align="middle" width="15.77067689999999pt" height="14.15524440000002pt"/>)|
| <img alt="$\hat{x}_{n, n-1}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/d49a6d1c13901555bdc3538e0aa68892.svg?de73c6cbd8" align="middle" width="46.37773469999998pt" height="22.831056599999986pt"/>  | is the previous estimate of <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?deaad233e9" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/> that was made at time <img alt="$n-1$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/efcf8d472ecdd2ea56d727b5746100e3.svg?62a9ef3236" align="middle" width="38.17727759999999pt" height="21.18721440000001pt"/> (the estimate was made after taking the measurement <img alt="$z_{n-1}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/9431fe18d89f4aed4379a6e4d6339c27.svg?57feed4304" align="middle" width="32.59724489999999pt" height="14.15524440000002pt"/>)   |
| <img alt="$\hat{x}_{n+1, n}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/e7e21ca4db21279d24f0c7f0b1e055ea.svg?237dac9bb3" align="middle" width="46.19508794999999pt" height="22.831056599999986pt"/>  | is the estimate of the future state (<img alt="$n+1$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/3f18d8f60c110e865571bba5ba67dcc6.svg?67a417caa6" align="middle" width="38.17727759999999pt" height="21.18721440000001pt"/>) of <img alt="$x$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/332cc365a4987aacce0ead01b8bdcc0b.svg?efaf7fb73" align="middle" width="9.39498779999999pt" height="14.15524440000002pt"/>. The estimate is made at the time <img alt="$n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/55a049b8f161ae7cfeb0197d75aff967.svg?13959bc3ad" align="middle" width="9.86687624999999pt" height="14.15524440000002pt"/>, right after the measurement <img alt="$z_n$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a5877527319d66afa46d823ccc099bf4.svg?9283f43678" align="middle" width="15.77067689999999pt" height="14.15524440000002pt"/>. In other words, <img alt="$x_{n, n+1}$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a41bbe848dcc636e1a54404dafd313a1.svg?6219d7823" align="middle" width="46.19508794999999pt" height="14.15524440000002pt"/> is a predicted state  |

<p align="center"> <b> The Five Kalman Filter Equations: </b> </p>



<p align="center"> State Update Equation: </p>

<p align="center"><img alt="$$&#10;\hat{x}_{n, n} = \hat{x}_{n, n-1} + K_n (z_n - \hat{x}_{n, n-1})&#10;$$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/9d58e6d2d9e2a8322083dbc5139bc761.svg?4712d70c3c" align="middle" width="239.15936715pt" height="17.031940199999998pt"/></p>


<p align="center"> State Extrapolation Equations: </p>

<p align="center"><img alt="$$&#10;\hat{x}_{n+1, n} = \hat{x}_{n, n} + \Delta t \hat{ \dot{x} }_{n, n}&#10;$$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/c3aec3262a77253dfba57edcf57fd410.svg?9338a54d7d" align="middle" width="168.58473555pt" height="20.019601799999997pt"/></p>

<p align="center"><img alt="$$&#10;\hat{\dot{x}}_{n+1, n} = \hat{\dot{x}}_{n, n}&#10;$$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/b7f913249a32ba2247e83557ee76b38b.svg?1c1de18494" align="middle" width="98.4857643pt" height="20.019601799999997pt"/></p>


<p align="center"> Kalman Gain Equation: </p>

<p align="center"><img alt="$$&#10;K_n = \frac{p_{n, n-1}}{p_{n, n-1} + r_n}&#10;$$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/6e2e96252b7e4a9e211c1c7ec0e81ce8.svg?932d2e4d71" align="middle" width="129.3299535pt" height="34.177354199999996pt"/></p>

<p align="center"> Covariance Update Equation </p>

<p align="center"><img alt="$$&#10;p_{n, n} = (1-K_n)p_{n, n-1}&#10;$$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/e8eca79b117d39eba75fe796b5c668cb.svg?5c18cc7e80" align="middle" width="160.4245632pt" height="17.031940199999998pt"/></p>

<p align="center"> Covariance Extrapolation Equation </p>

<p align="center"><img alt="$$&#10;p_{n+1, n} = p_{n, n}&#10;$$" src="https://cdn.jsdelivr.net/gh/l-winston/SLAM@master/svgs/a26857bfa3b40121fbd5f12c843f9f67.svg?a12a39cf1e" align="middle" width="96.23692154999999pt" height="11.780795399999999pt"/></p>

Next, we take a measurement, tweak our estimation of the state, and then once again predict the future state.

<p align="center">
<img width="100% "alt="Estimation Algorithm Flowchart" src="https://www.kalmanfilter.net/img/AlphaBeta/ex2_estimationAlgorithm.png">
</p>

# Sources & Resources
1. [How does Autonomous Driving Work? An Intro into SLAM](https://towardsdatascience.com/slam-intro-fd833ef29e4e)
2. [Simultaneous Localization and Mapping](https://en.wikipedia.org/wiki/Simultaneous_localization_and_mapping)
3. [MatLab: Understanding Kalman Filters](https://www.youtube.com/watch?v=mwn8xhgNpFY&list=PLn8PRpmsu08pzi6EMiYnR-076Mh-q3tWr)
4. https://www.kalmanfilter.net/default.aspx
