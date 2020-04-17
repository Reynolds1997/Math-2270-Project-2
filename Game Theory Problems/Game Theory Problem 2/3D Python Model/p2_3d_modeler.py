'''
======================
3D surface (color map)
======================

Demonstrates plotting a 3D surface colored with the coolwarm color map.
The surface is made opaque by using antialiased=False.

Also demonstrates using the LinearLocator and custom formatting for the
z axis tick labels.
'''

from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt
from matplotlib import cm
from matplotlib.ticker import LinearLocator, FormatStrFormatter
import numpy as np
from p2_solver import Problem2


fig = plt.figure()
ax = fig.gca(projection='3d')


X,Y,Z = Problem2.CalculateBestSalePercents() 

# Plot the surface.
#surf = ax.plot_surface(X, Y, Z, cmap=cm.coolwarm,
#                       linewidth=0, antialiased=False)


# RdYlBu 
cm = plt.cm.get_cmap('inferno')
surf = ax.scatter(X, Y, zs=Z, zdir='z', s=20, c=Z, depthshade=False, cmap = cm)


# Customize the z axis.
ax.set_zlim(0, 1.01)
ax.zaxis.set_major_locator(LinearLocator(10))
ax.zaxis.set_major_formatter(FormatStrFormatter('%.02f'))

ax.set_xlabel('Small Center Sale Prob.')
ax.set_ylabel('Large Center Sale Prob.')
ax.set_zlabel('Small Center Business Share')

# Add a color bar which maps values to colors.
fig.colorbar(surf, shrink=0.5, aspect=5)

plt.show()
