const mainUrl = "https://localhost:7266/api/students";

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
        firstName: document.getElementById("firstName").value,
        lastName: document.getElementById("lastName").value
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
