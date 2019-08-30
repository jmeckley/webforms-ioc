This solution contains 3 projects
1. WebApi - an example of dependency injection for MS MVC and MS WebApi
2. WebApplication - an example of dependnecy injection form WebForms
3. WebApplicationCore - the business functionality of WebApplication, with no references to System.Web or WebForms. The same concept could be applied to WebApi.


The exmaple uses Unity because the current application is using Unity. My personal preference is StructureMap as there are many convience classes and methods available to simplify
service registration. Unity is extremely explicit and does not favor convention over configuration. We would be responsible for designing convetions, as can be seen in 
WebbApplication/App_Start/UnityRegistration.cs. 

Another IoC option which is popular and performant is Ninject.



Ms MVC WebApi treats IoC as a first class concept. By implementing our own IDependencyResolver we have support for child containers (a container per request). Implementing our own 
IHttpControllerActivator we are able to resolve API controllers directly from the container that contains constructor arguemnents.
MS MVC does not afford us the same luxury. In this instance we need to retrofit child container per request into the legacy framework. Therefor we have the Mvc.IDependencyResolver, not be
confused with the Http.IDependencyResolver implementation. There are also a large number of dependencies for MVC which are not intuitively resolvable. The IsRegistered class helps us with this.

There is also a techinque for injecting services into RazorViews. However, that is not part of this example. That is a deliberate design choice. By definition, views should only get and set data 
into the view. The view should not contain logic or complex operations that would require additional components. It is responsibilty of the Controller to handle the logical operations.




MS Webforms is the 1st iteration of Asp.Net web appliaction development. This framework was never designed for depedency injection, complex application development, or testabilty. Therefore we 
must shoehorn in dependency injection if we want any form of testabilty, application composition, or funcitional encapsulation. What I have proposed is one approach to introducing IoC into webforms.
If follows an approach similar to MS MVC.

The primary difference between MVC and WebForms is who is responible for initiating the calls. 
For MVC the route resolves a controller, the controller resolves an action (render view, rediect, etc.)
For Webforms the route resolves a Page, the page resolves a presenters, the page tells the presenter what to do.

With WebForms think of the Page as a View and the Presenter as the Controller. It's not possible to unit test a WebForms Page object, therefore we want to keep the Page code behind as simple 
as possible. We want to move all the logic and processing out of the Page(View) and into the Presenter. This is also why the WebApplication.Core has no references to Sytem.Web, Asp.Net, or Webforms.

In this particular design we are trying to use a 1:1 mapping of views to presenters. That may not always be the case, but it should be the goal to keep the focus on single responsilbity and 
logical enacapsulation.


/*Before Running*/
Setup database. 
1. connect to your local database
   you may need to update the connection strings in the WebApi and WebApplication projects
2. execute the init_database.sql script