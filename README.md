# Interpolation

A simple program to visualize Polynomial Interpolation

## What you can do

### Lagrange Polynomial Interpolation

You will put the set of data points in the datagridview

  you can put them one by one or copy and paste from other table, take into consideration that the data points
  have to be in the rigth format to copy and paste (X and Y values should be separeted by a "tab") see exemple bellow:
  
-1.00 0.038  
-0.80	0.058  
-0.60	0.100  
-0.40	0.200  
-0.20	0.500  
0 1.00  
0.20	0.500  
0.40	0.200  
0.60	0.100  
0.80	0.058  
1.00	0.038

  After you have you data points in the table, you can calculate a interpolation point for a specific X by clinking
  in the "FOR X" button.
  Or you can set a X interval and the program will calculate all interpolation point between MAX and MIN X value 
  in the table with th given X interval. Cliking in the "Lagrange Interpolation" button, the resulting Interpolation points will be
  displayed in the table and the graph will be displayed on the side.(Dont forget to input a Y interval,minimum and maximum for the 
  displaying of the graph)
  
  Here you can see how will look like with the example data points:
  
  ![Lagrange Polynomial Interpolation](https://github.com/carineallen/Interpolation/blob/master/Interpolation/images/Interpolation_2.PNG)
  
 ### Cubic Spline Interpolation
      
  By clinkig the "Cubic Splines" Button it will show the graph of the given data points by Cubic Splines interpolation.
  You can calculate a interpolation point for a specific X by clinking in the "FOR X" button.
    
  Here is what will look for the given example data points:
    
  ![Cubic Splines Interpolation](https://github.com/carineallen/Interpolation/blob/master/Interpolation/images/Interpolation_3.PNG)
  
  
  
