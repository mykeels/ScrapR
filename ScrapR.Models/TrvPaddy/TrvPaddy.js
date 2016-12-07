$ = jQuery;

String.isNullOrEmpty = function (str) {
    return !str || (str == '');
}

var localData = { "trips": [{ "airportOrigin": "Lagos, NG - Murtala Muhammed (LOS)", "airportDestination": "Abuja, NG - Nnamdi Azikiwe Intl (ABV)", "departureDate": "Sat, 03 Dec 2016", "departureTime": "", "returnDate": "Tue, 06 Dec 2016", "returnTime": "" }, { "airportOrigin": "Abuja, NG - Nnamdi Azikiwe Intl (ABV)", "airportDestination": "London, GB - Heathrow (LHR)", "departureDate": "Fri, 09 Dec 2016", "departureTime": null, "returnDate": "", "returnTime": "" }], "adults": 1, "children": 0, "infants": 0, "tripClass": "all", "tripType": "multi" }

function setFlightsData(query) {
    if (typeof query == "string") {
        query = JSON.parse(query);
        this.argsIsString = true;
    }
    if (query && query.trips && query.trips.length > 0) {
        $("#from-field").val(query.trips[0].airportOrigin);
        $("#to-field").val(query.trips[0].airportDestination);
        $("#departure-date-field").val(query.trips[0].departureDate);
        $("#departure-time-of-day-field").val(query.trips[0].departureTime);
        $("#adults-field").val(query.adults);
        $("#children-field").val(query.children);
        $("#infants-field").val(query.infants);
        $("#cabin-class-field").val(query.tripClass);
        if (!String.isNullOrEmpty(query.trips[0].returnDate)) {
            $("#return-date-field").val(query.trips[0].returnDate);
            $("#return-time-of-day-field").val(query.trips[0].returnTime);
        }
        $("#flight-search-button").click();
    }
}

function getFlightsData() {
    return JSON.stringify(currPage.flightsCache);
}