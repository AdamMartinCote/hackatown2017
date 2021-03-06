/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
var app = {
    // Application Constructor
    initialize: function() {
        this.bindEvents();
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // 'load', 'deviceready', 'offline', and 'online'.
    bindEvents: function() {
        document.addEventListener('deviceready', this.onDeviceReady, false);
    },
    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady: function() {
        console.log('Received Device Ready Event');
        console.log('calling setup push');
        app.setupPush();


        // NOTIFICATION in APP
        /*
        navigator.notification.alert(
            "Message notification test", // message
            null, // callback
            "PUSH NOTIFICATION TEST", // title
            'Ok' // buttonName
        );
        */

        // Geolocalisation
        if ("geolocation" in navigator) {
            /* geolocation is available */
            /*
            console.log("Geolocalisation possible");

            var node = document.createElement("p");
            var textnode = document.createTextNode("Geolocalisation possible");
            var counter = 0;
            node.appendChild(textnode);
            document.getElementById("registration").appendChild(node);

            navigator.geolocation.watchPosition(function(position) {
                counter++;
                var node = document.createElement("p");
                var textnode = document.createTextNode("longitude:" + position.coords.longitude + " | latitude:" + position.coords.latitude + " counter:" + counter);
                node.appendChild(textnode);
                document.getElementById("registration").appendChild(node);

            });
            */
        } else {
            /* geolocation IS NOT available */
            /*
            console.log("Geolocalisation impossible");

            var node = document.createElement("h5");
            var textnode = document.createTextNode("Geolocalisation impossible");
            node.appendChild(textnode);
            document.getElementById("registration").appendChild(node);
            */
        }
    },
    findNewAlert: function() {
        console.log("envoie demande ajax");
        // ICI findNewAlert()
        setTimeout(app.findNewAlert, 7000);
        app.scanServer();
    },
    isAlertAnswered: function() {
        console.log("envoie demande ajax");
        app.askIsHelperAnswered();
        app.scanServer();
    },

    askIsHelperAnswered: function() {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function() {
            if (this.status == 404) {
                console.log("aucune requête en cours");
            } else if (this.readyState == 4 && this.status == 200) {
                var convertedJson = JSON.parse(this.responseText);
                console.log(convertedJson);
                if (convertedJson) {
                    document.getElementById("aidantEnChemin").style.display = "block";
                    document.getElementById("aidantOntRecuAlerte").style.display = "none";
                    app.obtainHelperDistance();
                } else {
                    setTimeout(app.isAlertAnswered, 5000);
                }
            }
        };

        xhttp.open("GET", "http://heroes.gear.host/api/alerte/isAlertAnswered?idInitiateur=1", true);
        xhttp.send();
    },

    obtainHelperDistance: function() {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function() {
            if (this.status == 404) {
                console.log("aucune requête en cours");
            } else if (this.readyState == 4 && this.status == 200) {
                var convertedJson = JSON.parse(this.responseText);
                console.log(convertedJson);
                if (convertedJson) {
                    document.getElementById("rayonAidant").innerHTML = "à " + convertedJson + " M";
                    setTimeout(app.obtainHelperDistance, 3000);
                } else {
                    setTimeout(app.obtainHelperDistance, 3000);
                }
            }
        };

        xhttp.open("GET", "http://heroes.gear.host/api/alerte/getHelperDistance?uid=1", true);
        xhttp.send();
    },

    scanServer: function() {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function() {
            if (this.status == 404) {
                <!-- aucun json correspondant sur le serveur -->
                console.log("aucune requête en cours");
            } else if (this.readyState == 4 && this.status == 200) {
                <!-- une alerte a été trouvée -->
                var convertedJson = JSON.parse(this.responseText);
                console.log(convertedJson);
                window.location.href = 'alertBox.html?longitude=' + convertedJson["longitude"] + '&latitude=' + convertedJson["latitude"] + '&uid=' + convertedJson["uid"];
            }
        };

        xhttp.open("GET", "http://heroes.gear.host/api/alerte/FindNewAlert?id=1", true);
        xhttp.send();
    },

    setupPush: function() {
        console.log('calling push init');
        var push = PushNotification.init({
            "android": {
                "senderID": "XXXXXXXX"
            },
            "browser": {},
            "ios": {
                "sound": true,
                "vibration": true,
                "badge": true
            },
            "windows": {}
        });
        console.log('after init');

        push.on('registration', function(data) {
            console.log('registration event: ' + data.registrationId);

            var oldRegId = localStorage.getItem('registrationId');
            if (oldRegId !== data.registrationId) {
                // Save new registration ID
                localStorage.setItem('registrationId', data.registrationId);
                // Post registrationId to your app server as the value has changed
            }

            var parentElement = document.getElementById('registration');
            var listeningElement = parentElement.querySelector('.waiting');
            var receivedElement = parentElement.querySelector('.received');

            listeningElement.setAttribute('style', 'display:none;');
            receivedElement.setAttribute('style', 'display:block;');
        });

        push.on('error', function(e) {
            console.log("push error = " + e.message);
        });

        push.on('notification', function(data) {
            console.log('notification event');
            navigator.notification.alert(
                data.message, // message
                null, // callback
                data.title, // title
                'Ok' // buttonName
            );
        });
    }
};