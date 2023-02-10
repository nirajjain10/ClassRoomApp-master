# ClassRoomApp
Create ASP.NET Core Web API project in Visual Studio Code(VS Code) — part 1
ASP.NET Core is a popular framework to build web applications on .Net platform. ASP.NET core is an open-source version of ASP.NET framework available on Windows, Linux, macOS, and Docker. In this tutorial, we’ll create web API in ASP.NET Core using VSCode. In the next part of this series we’ll use Swagger (aka OpenAPI ) to test the API endpoints we’ll be creating in this tutorial.

Prerequisites:
Install .NET Core 3.1 SDK or later. (click here)
Install Visual Studio Code. (click here)
Install C# extension in Visual Studio Code (for more information regarding how to install the extension in your Visual Studio Code click here)
Install NuGet Package Manager VS Code extension (click here)[Optional]
Install Visual Studio IntelliCode VS Code extension (click here) [Optional]
Create the App:
Open command prompt/terminal and change directory to the desired location. Create a directory using the following command:

mkdir ClassroomApp.

create folder(directory)
run:

dotnet new webapi

run: dotnet new webapi
after running this command, you’ll see the new project has been created with the number of generated files. Generated code has already created one controller for us — “WeatherForecastController.cs”. WeatherForecastController has one GET Web API (GET WeatherForecast). We’ll learn more about creating our own endpoint and using the created endpoints in the following sections.

To open the Web API project in Visual Studio Code run the following command:

code .

run: code .
this command will open Visual Studio Code instance:


Visual Studio Code window
In .NET Core framework Web APIs are implemented in Controllers. Controllers are classes derived from the ControllerBase class. Web API project could be consisting of one or more controllers, all of these controllers need to be derived from ControllerBase class. ControllerBase class has various properties which we can use to handle REST requests. The Web API project which we have created from the template provides us starter Controller — WeatherForecastController as shown in below the following screenshot:


WeatherForecastController is decorated with ApiController attribute. Applying ApiController attribute enables some API specific behaviors such as attribute routing, Automatic HTTP 400 series responses, binding the source parameters from the request URLs, etc.

ASP.NET Core uses routing middleware to map the incoming request URL to the action that needs to be performed. The routes could be placed as attribute on the controller or action(method in controller). This way of defining routes for actions is called attribute-routed actions. The starter controller provided in the generated template has attribute-routed actions:


APIs use the HTTP protocols like GET, POST, PUT, and DELETE to link the resources with the actions that need to be performed. ASP.NET Core framework has following HTTP verbs which can be placed as attributes of the actions:

[HttpGet]
[HttpPost]
[HttpPut]
[HttpPut]
[HttpDelete]
[HttpHead]
[HttpPatch]
In the starter code of the template, they have defined one action Get() in the WeatherForecastController. Get() action has HttpGet attribute which indicates this action uses the HTTP GET protocol.


Now, let’s create our own controller and REST API endpoints.

In this tutorial, we’ll create two simple REST API endpoints for our Classroom application. One API to add Students in the classroom and another to get all the students in the classroom.

First, create the data model for Student:

public class Student
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Grade {get; set;}
}
To keep things simple, we’ll not use any database (or framework like Entity Framework Core) to store the data. We’ll store data in application memory. Create IClassroomService interface and ClassroomService class deriving the IClassroomService to implement the actions in the Classroom controller. Create IClassroomService and ClassroomService class in the Services folder for easier code maintenance.

IClassroomService and ClassroomService

public interface IClassroomService    
{
    bool AddStudent(Student student);
    IEnumerable<Student> GetAllStudents();    
}
public class ClassroomService : IClassroomService
{
    private IList<Student> _students;
public ClassroomService()
    {
        _students = new List<Student>();
    }
public bool AddStudent(Student student)
    {
        if(student != null)
        {
            _students.Add(student);
            return true;
        }
        return false;
     }
public IEnumerable<Student> GetAllStudents()
    {
        return _students;
    }
}
To create new controller, create new class ClassRoomController which derives the ControllerBase class. For code maintenance and readability create this class in Controllers folder. To utilize the .NET Core provided API handling and attribute routing, decorate the ClassRoomController with ApiController attribute. Define the default route for the actions in the controller with Route attribute.

[ApiController]
[Route("api/Classroom/Student")]
public class ClassRoomController : ControllerBase
{ 
}
ASP.NET supports the dependency Ingestion design pattern to achieve Inversion of Control(IoC). We need to bind the ClassroomService instance against IClassroomService. Services can be added to the IoC container in the Startup.cs class which is generated as part of the Web API project template.


Actions in controllers of Web APIs in .NET Core can return one of the following return type:

Specific Type: this is the simplest data type, which return any if the primitive or complex data type (like int, string, or custom defined object type).
IActionResult: IActionResult should be used when multiple ActionResult types are possible in an action. Returning IActionResults requires to decorate the action with [ProducesResponseType] attribute.
ActionResult<T>: during the execution of some of the actions, the specific type could not be returned due to various reasons like a failure in model verifications or unexpected exceptions during execution. In such cases, ActionResult<T> should be returned which enables to return of either type deriving the ActionResult or specific type.
Implement HttpGet and HttpPost action in ClassRoomController using ClassroomService:

[ApiController]
[Route("api/Classroom/Student")]
public class ClassRoomController : ControllerBase
{
    private IClassroomService _classroomService;
    
    public ClassRoomController(IClassroomService classroomService)
    {
        _classroomService = classroomService;
    }
    [HttpPost]
    public ActionResult<bool> AddStudent(Student student)
    {
        if(_classroomService == null)
            {
                return NotFound();
            }
        var result = _classroomService.AddStudent(student);
        return result;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        if(_classroomService == null)
        {
            return NotFound();
        }
        var result = _classroomService.GetAllStudents().ToList();
        return result;
   }
}
Your Web API in ASP.Net Core is ready!