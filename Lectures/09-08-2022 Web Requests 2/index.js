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
