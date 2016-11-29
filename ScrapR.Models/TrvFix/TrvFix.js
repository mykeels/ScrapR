function createSetTripType() {
    var $scope = angular.element('[data-ng-controller="FlightSearchFormCtrl as searchCtrl"]').scope();
    $scope.searchCtrl.setTripType = function (tripType) {
        this.tripType = tripType;
        this.currentTripType = tripType;
        this.isMultiCity = (tripType == "Circle");
        this.isReturnTrip = (tripType == "Return");
        $scope.$applyAsync();
    }
}

String.isNullOrEmpty = function (str) {
    return (!str || str == '');
}

function copyObject(objA, objB) {
    for (var key in objB) {
        if (objA.hasOwnProperty(key)) {
            var type = typeof objA[key];
            if (key == 'airport') objA[key] = objB[key];
            else if (type == 'string' || type == 'number' || type == 'boolean' || type == 'date') {
                objA[key] = objB[key];
            }
            else if (type == 'object') {
                copyObject(objA[key], objB[key])
            }
            else if (Array.isArray(objB[key])) {
                copyArray(objA[key], objB[key]);
            }
            //console.log(key, objA[key], objB[key]);
        }
    }
    return objA;
}

function copyArray(arrA, arrB) {
    for (var i = 0; i < arrB.length; i++) {
        var type = arrB[i];
        if (!arrA[i]) arrA.push(arrB[i]);
        else {
            if (type == 'string' || type == 'number' || type == 'boolean' || type == 'date') {
                arrA[i] = arrB[i];
            }
            else if (type == 'object') {
                copyObject(arrA[i], arrB[i])
            }
            else if (Array.isArray(arrB[i])) {
                copyArray(arrA[i], arrB[i]);
            }
        }
    }
    return arrA;
}

function setFlightData(query) {
    if (!query) return;
    if (typeof query == 'string') query = JSON.parse(query); //convert from string to object when data is entered by the web scraper
    var $scope = angular.element('[data-ng-controller="FlightSearchFormCtrl as searchCtrl"]').scope();
    var searchCtrl = $scope.searchCtrl;
    copyObject(searchCtrl, query);
    createSetTripType();
    searchCtrl.setTripType(query.tripType);
    $scope.$applyAsync();
    searchCtrl.submit();
    return searchCtrl;
}