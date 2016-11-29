//oneway
var objOneWay = { "locale": { "country": "NG", "currentLocale": "en" }, "travellers": { "adults": 1, "children": 0, "infants": 0 }, "tripType": "oneway", "itineraries": [{ "departDate": "2016-11-24", "id": "8cea680d-e86d-492f-a5aa-121a5e9c74ae", "returnDate": "2016-11-26", "destination": { "display": "Abuja (ABV)", "value": { "airport": "Nnamdi Azikiwe International Airport", "city": "Abuja", "code": "ABV", "country": "Nigeria", "countryIata": "NG", "iata": "ABV", "locationId": "airport_ABV", "type": "airport" } }, "origin": { "display": "Lagos (LOS)", "value": { "airport": "Murtala Muhammed International Airport", "city": "Lagos", "code": "LOS", "country": "Nigeria", "countryIata": "NG", "iata": "LOS", "locationId": "airport_LOS", "type": "airport" } } }] }

//return
var objReturn = { "tripType": "return", "isNewSession": false, "travellers": { "adults": 2, "children": 0, "infants": 0 }, "moreOptions": {}, "itineraries": [{ "id": "4b032ad8-f84b-4f18-84a7-754e411c20ad", "departDate": "2016-11-24", "returnDate": "2016-11-26", "$$hashKey": "object:45", "origin": { "display": "Lagos (LOS)", "value": { "airport": "Murtala Muhammed International Airport", "city": "Lagos", "code": "LOS", "country": "Nigeria", "countryIata": "NG", "iata": "LOS", "locationId": "airport_LOS", "type": "airport" } }, "destination": { "display": "Abuja (ABV)", "value": { "airport": "Nnamdi Azikiwe International Airport", "city": "Abuja", "code": "ABV", "country": "Nigeria", "countryIata": "NG", "iata": "ABV", "locationId": "airport_ABV", "type": "airport" } } }], "locale": { "country": "NG", "currentLocale": "en", "locales": [] }, "validation": { "travellersValid": true } }

//multi
var objMulti = { "tripType": "multi", "isNewSession": false, "travellers": { "adults": 2, "children": 0, "infants": 0 }, "moreOptions": {}, "itineraries": [{ "id": "4b032ad8-f84b-4f18-84a7-754e411c20ad", "departDate": "2016-11-24", "returnDate": "2016-11-26", "$$hashKey": "object:45", "origin": { "display": "Lagos (LOS)", "value": { "airport": "Murtala Muhammed International Airport", "city": "Lagos", "code": "LOS", "country": "Nigeria", "countryIata": "NG", "iata": "LOS", "locationId": "airport_LOS", "type": "airport" } }, "destination": { "display": "Abuja (ABV)", "value": { "airport": "Nnamdi Azikiwe International Airport", "city": "Abuja", "code": "ABV", "country": "Nigeria", "countryIata": "NG", "iata": "ABV", "locationId": "airport_ABV", "type": "airport" } } }, { "id": "c877e92b-7700-431f-8638-7f70d796ad9d", "departDate": "2016-11-26", "returnDate": "2016-11-30", "$$hashKey": "object:1993", "origin": { "value": { "type": "airport", "city": "Abuja", "airport": "Nnamdi Azikiwe International Airport", "iata": "ABV", "code": "ABV", "country": "Nigeria", "countryIata": "NG", "locationId": "airport_ABV" }, "display": "Abuja (ABV)" }, "destination": { "value": { "type": "city", "city": "Johannesburg", "airport": "All Airports", "iata": "JNB", "code": "JNB", "country": "South Africa", "countryIata": "ZA", "locationId": "ZA_city_JNB" }, "display": "Johannesburg (JNB)" } }], "locale": { "country": "NG", "currentLocale": "en", "locales": [] }, "validation": { "travellersValid": true } }


function setFlightData(query) {
    if (typeof query == "string") {
        query = JSON.parse(query);
    }
    var $scope = angular.element(document.querySelector(".search-form__row")).scope();
    var formCtrl = $scope.$parent.searchFormController;
    formCtrl.searchData.tripType = query.tripType;
    for (var i = 0; i < query.itineraries.length; i++) {
        if (!formCtrl.searchData.itineraries[i]) {
            formCtrl.addFlight();
            $scope.$apply();
        }
        formCtrl.searchData.itineraries[i].departDate = query.itineraries[i].departDate;
        formCtrl.searchData.itineraries[i].returnDate = query.itineraries[i].returnDate;
        formCtrl.searchData.itineraries[i].destination = query.itineraries[i].destination;
        formCtrl.searchData.itineraries[i].origin = query.itineraries[i].origin;
    }
    formCtrl.searchData.travellers = query.travellers;
    $scope.$apply();
    /*for (var i = 0; i < query.itineraries.length; i++) {
        setLocationElem(query.itineraries[i], i % 2 == 0 ? 1 : 0, i);
        setLocationElem(query.itineraries[i], i % 2 == 0 ? 0 : 1, i);
    }*/
    loopSlow(query.itineraries, function (itinerary, i) {
        document.querySelector(".clear-field__icon").click();
        setLocationElem(itinerary, i % 2 == 0 ? 1 : 0, i);
        setLocationElem(itinerary, i % 2 == 0 ? 0 : 1, i);
        document.querySelector(".clear-field__icon").click();
        $scope.$apply();
    }, 0, function () {
        document.querySelector(".clear-field__icon").click();
        formCtrl.submitSearch();
        console.log("submitSearch");
    });
    return JSON.stringify(formCtrl.searchData);
}

function setLocationElem(itinerary, index, i) {
    console.log((i * 2) + index);
    var origElem = angular.element(document.querySelectorAll(".clear-field__icon")[(i * 2) + index]).triggerHandler("mousedown");
    var firstOption = angular.element(document.querySelector('[ng-mouseenter="selectActive($index)"]'));
    firstOption.scope().match.label = index == 0 ? itinerary.origin : itinerary.destination;
    firstOption.scope().match.model = index == 0 ? itinerary.origin : itinerary.destination;
    firstOption.scope().$parent.selectMatch(0);
}

function loopSlow(arr, fn, i, finalFn) {
    i = i || 0;
    if (i < arr.length) {
        try { fn(arr[i], i); } catch (ex) { finalFn(); }
        setTimeout(function () { loopSlow(arr, fn, i + 1, finalFn) }, 1500);
    }
    else {
        finalFn();
    }
}

function getAirlineScopeFnKeys($scope) {
    var arr = [];
    for (var key in $scope) {
        if (typeof $scope[key] == 'function' && !(key.indexOf('$') == 0) && !(key == 'constructor')) arr.push(key);
    }
    return arr;
} //gets all keys within the flightDetailsController that represent important functions to be resolved into the itinerary object

function getAirlineScopeItineraries() { //gives you the list of flight-search results (also called price itineraries)
    try {
        var arr = [];
        var elems = document.querySelectorAll(".ts-itin__airline");
        for (var i = 0; i < elems.length; i++) {
            var elem = elems[i];
            var $scope = angular.element(elem).scope();
            var keys = getAirlineScopeFnKeys($scope);
            for (var j = 0; j < keys.length; j++) {
                var key = keys[j];
                $scope.itinerary[key] = $scope[key]();
            }
            arr.push($scope.itinerary);
        }
        return JSON.stringify(arr);
    }
    catch (ex) {
        alert(JSON.stringify(ex));
        return "[]";
    }
    //return arr;
}

function selectAirlineItinerary(itinerary) {
    var $scope = angular.element(document.querySelector(".flight-details__continue")).scope();
    var $flightCtrl = $scope.flightDetailsController;
    $flightCtrl.selectedItinerary = itinerary;
    $flightCtrl.priceItinerary({ itin: itinerary });
}

window.onerror = function (msg, url, line, col, error) {
    var extra = !col ? '' : '\ncolumn: ' + col;
    extra += !error ? '' : '\nerror: ' + error;
    alert("Error: " + msg + "\nurl: " + url + "\nline: " + line + extra);
    var suppressErrorAlert = true;
    return suppressErrorAlert;
};