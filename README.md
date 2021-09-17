# ShopCore

This is a ASP.NET web application created in Visual Studio 17 that covers simple basic functionalities of a web shop for video games which I'm creating for practice. It is currently not connected to any database and is instead working with hardcoded test data. The solution is divided into two parts: the Web API and Frontend as seen below:

![Solution](https://i.imgur.com/eAh3R2W.png)

The Web API contains controllers, models/entities that are representing the future database and other classes that handle client interaction such as error filters and interfaces for dependancy injection. The Frontend contains pages created in pure HTML and CSS (no Razor for extra challenge) while the client-side scripting was done through JavaScript and jQuery. 

In order for the Frontend to communicate with the API, I have enabled CORS in the Startup.cs as shown below:

![CORS](https://i.imgur.com/OYsWEyE.png)

In order to test the web app both ShopCore.WebAPI and ShopCore.Frontend need to be configured to start at the same time (done in the solution settings:

![Startup](https://i.imgur.com/1Flub2Q.png)

# Functionalities

The web shop can be used by three categories of people: users without an account (who can simply browse the shop and look at what's in store), registered users (who can add items to cart and make orders) and admins (who can perform CRUD operations on the projects in the simulated database). The home page will always present 3 different randomly selected products for display to the visitor and upon clicking any of them, more detail will be shown to the user.

![Index](https://i.imgur.com/ubys98p.png)
![GameDetails](https://i.imgur.com/EzvmLbO.png)

Users can search through all available games by name with other parameters being added soon. There is also a personalization aspect implemented with registered users being able to change every aspect of their account from their username to their avatar.

![Profile](https://i.imgur.com/hzUJVp8.png)
![AvatarEdit](https://i.imgur.com/klj03OI.png)

Users also have the ability to edit their cart and checkout (though no payment systems have been implemented at this point) as well as look at all their previous orders down to the individual item.

![Orders](https://i.imgur.com/X26ddWX.png)
![OrderDetails](https://i.imgur.com/qWMbqXv.png)

The website is also responsive and adapts to a variety of devices and resolutions.

![S9Layout](https://i.imgur.com/K1hdrFp.png)
![XLayout](https://i.imgur.com/eK0ZE4w.png)
