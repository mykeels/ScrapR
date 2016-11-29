function setFlightData(query) {
    if (typeof query == "string") {
        query = JSON.parse(query);
    }
    var $scope = angular.element($(".search-form__row")[0]).scope();
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
        $(".clear-field__icon")[0].click();
        setLocationElem(itinerary, i % 2 == 0 ? 1 : 0, i);
        setLocationElem(itinerary, i % 2 == 0 ? 0 : 1, i);
        $(".clear-field__icon")[0].click();
        $scope.$apply();
    }, 0, function () {
        $(".clear-field__icon")[0].click();
        formCtrl.submitSearch();
        console.log("submitSearch");
    });
    return JSON.stringify(formCtrl.searchData);
}

function setLocationElem(itinerary, index, i) {
    console.log((i * 2) + index);
    var origElem = angular.element($(".clear-field__icon")[(i * 2) + index]).triggerHandler("mousedown");
    var firstOption = angular.element($('[ng-mouseenter="selectActive($index)"]')[0]);
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
        var elems = $(".ts-itin__airline");
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
    var $scope = angular.element($(".flight-details__continue")[0]).scope();
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