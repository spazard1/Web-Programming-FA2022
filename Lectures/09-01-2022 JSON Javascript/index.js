parseJson = () => {

  let errorElement = document.getElementById("error");
  errorElement.innerHTML = "";

  let changeColorObject;
  try {
    changeColorObject = JSON.parse(document.getElementById("jsonTextArea").value);
  } catch (error) {
    errorElement.innerHTML = error;
    return;
  }
  
  if (changeColorObject.stevenColor) {
    document.getElementById("steven").style.color = changeColorObject.stevenColor;
  } else {
    errorElement.innerHTML = "Your JSON didn't have the key 'stevenColor'.";
  }

}

window.onload = () => {
  
  // don't call parseJson, just set the reference to it
  document.getElementById("parseButton").onclick = parseJson;
}
