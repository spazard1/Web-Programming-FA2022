var serverUrl = "api/hobbits";

function startFunc() {
    document.getElementById('results').innerHTML = 'Running tests...';

    async.waterfall([
        function (cb) { runResponseTest("No Data created yet", serverUrl, "GET", [], cb); },
        function (cb) { runResponseTest("Create a hobbit", serverUrl + "?hobbit=Frodo", "POST", "Frodo", cb); },
        function (cb) { runResponseTest("Create another hobbit", serverUrl + "?hobbit=Sam", "POST", "Sam", cb); },
        function (cb) { runResponseTest("Two hobbits created", serverUrl, "GET", ["Frodo", "Sam"], cb); },
        function (cb) { runRequestIdTest("request-id header set", serverUrl, "GET", cb); },
        function (cb) { stopwatchTest("stopwatch header", serverUrl, "GET", cb); },
        function (cb) { resetTests(serverUrl, cb); }
    ], function (err, results) {
        if (err) {
            if (typeof err === "object") {
                createFail("An error status code came back from the server. " + JSON.stringify(err));
            } else {
                createFail("An error status code came back from the server. " + err);
            }
        }
    });
}

function runResponseTest(testName, serverUrl, method, expected, callback) {
    $.ajax(serverUrl,
        {
            method: method,
            success: function (data) {
                validateResponse(testName, expected, data);
                callback(null);
            },
            error: callback,
            contentType: 'application/json'
        }
    );
}

function runRequestIdTest(testName, serverUrl, method, callback) {
    $.ajax(serverUrl,
        {
            method: method,
            crossDomain: true,
            success: function (data, statusString, response) {
                var requestId = response.getResponseHeader("request-id");

                if (!requestId) {
                    createFail(testName + " - no request-id header found.");
                } else if (requestId === "local-id") {
                    createFail(testName + " - not using the correct request-id generator");
                } else if (requestId.length < 20 || requestId.indexOf('-') < 0) {
                    createFail(testName + " - not using the correct request-id generator");
                } else {
                    createPass(testName);
                }
                callback(null);
            },
            error: callback,
            contentType: 'application/json'
        }
    );
}

function stopwatchTest(testName, serverUrl, method, callback) {
    $.ajax(serverUrl,
        {
            method: method,
            crossDomain: true,
            success: function (data, statusString, response) {
                var stopwatch = response.getResponseHeader("stopwatch");
                var swRegex = /^\{Action Executing \d+\}\{Controller \d+\}\{Action Executed \d+\}$/;

                if (stopwatch && stopwatch.match(swRegex)) {
                    createPass(testName);
                } else {
                    createFail(testName + " did not match regex: /^\{Action Executing \d+\}\{Controller \d+\}\{Action Executed \d+\}$/", null, stopwatch);
                }
                callback(null);
            },
            error: callback,
            contentType: 'application/json'
        }
    );
}

function resetTests(serverUrl, callback) {
    $.ajax(serverUrl,
        {
            method: "DELETE",
            crossDomain: true,
            success: function (data, statusString, response) {
                callback(null);
            },
            error: callback,
            contentType: 'application/json'
        }
    );
}

function validateResponse(testName, expected, actual) {
    if (Array.isArray(expected) && !arraysEqual(expected, actual)) {
        createFail(testName, JSON.stringify(expected), JSON.stringify(actual));
    } else if (!Array.isArray(expected) && expected !== actual) {
        createFail(testName, expected, actual);
    } else {
        createPass(testName);
    }
}

function createPass(testName) {
    var results = document.getElementById('results');
    var result = document.createElement('div');
    result.className = "pass";
    result.appendChild(document.createTextNode(testName + " - PASS"));
    results.appendChild(result);
    results.appendChild(document.createElement('br'));
}

function createFail(testName, expected, actual) {
    var results = document.getElementById('results');
    var result = document.createElement('div');
    result.className = "fail";
    result.appendChild(document.createTextNode(testName + " - FAIL"));
    result.appendChild(document.createElement('br'));
    if (expected) {
        result.appendChild(document.createTextNode("Expected: " + expected + " --- Actual: " + actual));
        result.appendChild(document.createElement('br'));
    } else if (actual) {
        result.appendChild(document.createTextNode("Actual: " + actual));
        result.appendChild(document.createElement('br'));
    }
    results.appendChild(result);
    results.appendChild(document.createElement('br'));
}

function arraysEqual(a, b) {
    if (a === b) return true;
    if (a === null || b === null) return false;
    if (a.length !== b.length) return false;

    // If you don't care about the order of the elements inside
    // the array, you should sort both arrays here.

    for (var i = 0; i < a.length; ++i) {
        if (a[i] !== b[i]) return false;
    }
    return true;
}

window.onload = function () {
    document.getElementById('startButton').onclick = startFunc;
};