/*

REST
REpresentational State Transfer

1. Convention over Configuation
2. Stateless

CRUD
  Create, Read, Update, Delete


HTTP Verbs == HTTP Methods

GET - Read data from the server
POST - Create data on the server, return the data that was just created
DELETE - Delete/remove data from the server
PUT - Replace (or create) data on the server
PATCH (MERGE) - Update/edit data on the server

OPTIONS - For CORS, Cross-Origin Resource Sharing


How we talk to the server:

1. URL 
  Where do I want to go? What data to I want to get/create/delete?
  e.g. all students? a particular student?
2. Body == Payload
  What do we want to create or update?
  e.g. The name of a person with their address, etc.
3. Headers
  Metadata of the request
  e.g. the size of the request, the content type, the time of the request
  Content-Type: this is the mime type of the body (usually application/json)
  Accept: please server respond with this type, (it's a mime type)
  Content-Length: int in bytes of the size of the payload/body
  User-Agent: web browser identification
  Authorization: sending your credentials to the server
  

How the server responds to us:

1. Status Code
  200 - OK
  204 - NoContent
  404 - NotFound
2. Body == Payload
  The data we asked for, the data that we just created, etc.
3. Headers
  Metadata of the response
  e.g. the current time of the server, the size of the response, the content type of the response, etc.
  Location: used with redirect requests, where are you supposed to go? this is a url.
  Content-Type: this is the mime type of the body (usually application/json)
  Content-Length: int in bytes of the size of the payload/body
  Date: The time of the response


Status Codes

  XYY - X type of the status code, and then YY is the sub type

  100

  200 - OK
    201 - Created, meaning a successful POST request, some new data on the server
    202 - Accepted, work is not done yet, long running task on the server
    204 - NoContent, body is empty, usually for DELETE requests

  300 - Redirects "The content is somewhere else"
    301 - Moved Permanently, the page is never going to exist here again
    307 - Moved Temporarily, the page is going to be here, but look elsewhere for now

  400 - BadRequest, "It's YOUR fault" (the client's fault)
    401 - Unauthorized, invalid credentials, such as bad password
    403 - Forbidden, you credentials WERE accepted, but you can't do that thing (not enough permissions)
    404 - NotFound

  500 - InternalServerError, "It's the SERVER's fault"
    likely the server threw an exception
    503 - Service Unavailable, too many requests, server is out of resources

*/

const mainUrl = "https://simpleserverbethel.azurewebsites.net/values";

simpleSuccess = (data) => {
  document.getElementById("results").innerHTML = JSON.stringify(data);
}

runGet = () => {
  // fetch by default is GET
  fetch(mainUrl).then(response => {
    return response.json();
  }).then(responseJson => {
    simpleSuccess(responseJson);
  });
}

runPost = () => {
  fetch(mainUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json" // mime-type
    },
    body: JSON.stringify({
      value: document.getElementById("userValue").value
    })
  }).then(response => {
    return response.json();
  }).then(responseJson => {
    simpleSuccess(responseJson);
  });
}

runDelete = () => {
  fetch(mainUrl + "/" + document.getElementById("userIndex").value, {
    method: "DELETE"
  });
}

runPut = () => {
  fetch(mainUrl + "/" + document.getElementById("userIndex").value, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json" // mime-type
    },
    body: JSON.stringify({
      value: document.getElementById("userValue").value
    })
  }).then(response => {
    return response.json();
  }).then(responseJson => {
    simpleSuccess(responseJson);
  });
}

window.onload = () => {
  document.getElementById("getButton").onclick = runGet;
  document.getElementById("postButton").onclick = runPost;
  document.getElementById("deleteButton").onclick = runDelete;
  document.getElementById("putButton").onclick = runPut;

}
