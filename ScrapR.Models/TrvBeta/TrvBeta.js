String.IsNullOrEmpty = function (str) {
    return str == null || str == "";            
}

function camelize(str) {
  return str.replace(/(?:^\w|[A-Z]|\b\w)/g, function(letter, index) {
    return index == 0 ? letter.toLowerCase() : letter.toUpperCase();
  }).replace(/\s+/g, '');
}

function setTripData(query, tripType) {
    if (typeof query == "string") {
        query = JSON.parse(query);
    }
    $('#' + tripType + 'TripStartAirport').val(query.departureAirportId);
    $('#' + tripType + 'TripEndAirport').val(query.arrivalAirportId);
    $('[name="adult"]').val(query.adult || 1);
    if (query.children && query.children > 0) {
        $('[name="children"]').val(query.children);
    }
    if (query.infant && query.infant > 0) {
        $('[name="infant"]').val(query.infant);
    }
    $('[name="flightClass"]').val((query.flightClass || 'economy').toLowerCase());
    $('[name="departingDate"]').val(query.departingDate);
    if (!String.IsNullOrEmpty(query.returningDate)) $('[name="returningDate"]').val(query.returningDate);
    if (query.multiTrip && query.tripType == 3) {
        //multi trip
        for (var i = 0; i < query.multiTrip.length; i++) {
            var trip = query.multiTrip[i];
            var id = Number(i + 1);
            $('[fieldid="departureAirportCode' + id + '"]').val("city" + id + "A");
            $('#departureAirportCode' + id).val(trip.departureAirportId);
            $('[fieldid="destinationAirportCode' + id + '"]').val("city" + id + "B");
            $('#destinationAirportCode' + id).val(trip.arrivalAirportId);
            $('#departureDate' + id).val(trip.departingDate);
        }
    }
    return JSON.stringify(query);
}

function setOneTripData(query) {
    if (typeof query == "string") {
        query = JSON.parse(query);
    }
    $('#fromAirportOneTrip .typeahead').val("cityA");
    $('#toAirportOneTrip .typeahead').val("cityB");
    query.tripType = 1;
    return setTripData(query, 'one');
}

function setRoundTripData(query) {
    if (typeof query == "string") {
        query = JSON.parse(query);
    }
    $('#fromAirportRoundTrip .typeahead').val("cityA");
    $('#toAirportRoundTrip .typeahead').val("cityB");
    query.tripType = 2;
    return setTripData(query, 'round');
}

function setMultiTripData(query) {
    if (typeof query == "string") {
        query = JSON.parse(query);
    }
    query.tripType = 3;
    return setTripData(query, 'multi');
}

var query = {
    arrivalAirportId: 34979,
    departureAirportId: 35248,
    adult: 1,
    children: 0,
    infant: 0,
    departingDate: '22/11/2016',
    returningDate: '24/11/2016',
    flightClass: 'economy',
    tripType: 1,
    multiTrip: [
        {
            arrivalAirportId: 34979,
            departureAirportId: 35248,
            departingDate: '22/11/2016'
        },
        {
            arrivalAirportId: 34277,
            departureAirportId: 34979,
            departingDate: '26/11/2016'
        }
    ]
}

function getDefaultQuery() {
    return JSON.stringify(query);
}

function submitForm(selector) {
    var formData = $(selector).serializeArray();
    var flightSearchUrl = "/flight/asyncflightsearch";
    postDataToServer(formData, flightSearchUrl, function (data) {
        var message = data.message;
        if (data.status === 'SUCCESS') {
            var url = '/flight/searchresult';
            window.location.href = url;
        }else if(data.status === 'INFO'){
            alert(message);
        	$('#search-container').removeClass('hide').show();
        }else {
            alert(message);
            $('#search-container').removeClass('hide').show();
        }
        hideFlightLoadingModal();
        return false;
    }
);
    return {
        data: formData,
        url: flightSearchUrl
    }
}

function submitOneTripForm() {
    //$("#oneTripForm [type='submit']").click();
    return JSON.stringify(submitForm('#oneTripForm'));
    //return document.location.toString();
}

function submitRoundTripForm() {
    //$("#roundTripForm [type='submit']").click();
    return JSON.stringify(submitForm('#roundTripForm'));
    //return document.location.toString();
}

function submitMultipleTripForm() {
    //$("#multipleTripForm [type='submit']").click();
    return JSON.stringify(submitForm('#multipleTripForm'));
}

function postDataToServer(formData, postUrl,callback){
	jQuery.ajax({
	    url: postUrl,
	    type: 'POST',
	    data: formData,
	    timeout: 65000,
	    dataType : 'json',
	    success: function (data) {
	    	console.log('Data from server: '+JSON.stringify(data));
	    	callback(data);
	    },
	   error : function(data) {
		   console.error(data);
		   var newData ={
				   message : 'Sorry, unable to process request at the moment.',
				   status : 'ERROR',
		   }
		   callback(newData);
	   }
	}); 
}

function renderUrl(url) {
    return "https://travelbeta.com" + url;
}

function getFlightsInfo(query) {
    var queryType = "object";
    if (typeof query == "string") {
        query = JSON.parse(query);
        queryType = "string";
    }
    var arr = [];
    $(".xxnon-stop.subDetail").each(function () {
        var elem = this;
        var obj = {
            airline: {
                imageUrl: renderUrl($(elem).find("img").attr("src"))
            },
            trips: [],
            name: $(elem).find(".box-title").text(),
            query: query,
            price: $(elem).find(".price").text(),
            detailsUrl: renderUrl($(elem).find(".button.sky-blue1").attr("href")),
            totalDuration: ($(elem).find(".pull-right").text().split(":")[1] || ""),
            departureTime: ($(elem).find(".pull-right").next().text().split(":").slice(1).join(":") || "")
        };
        $(this).next().find("article").each(function () {
            var trip = {
                name: $(this).find(".box-title").text(),
                type: $(this).find(".button.box-title").text(),
                departureTime: ($(this).find(".take-off").text().replace("Take off", "") || ""),
                arrivalTime: ($(this).find(".landing").text().replace("landing", "") || ""),
                duration: ($(this).find(".total-time").text().replace("total time", "") || ""),
                departureAirport: ($(this).find(".detailed-features.booking-details").find(".text-center").not(".tb_to").first().text() || ""),
                arrivalAirport: ($(this).find(".detailed-features.booking-details").find(".text-center").not(".tb_to").last().text() || ""),
                details: {

                }
            }
            $(this).find(".term-description").find("dt").each(function () {
                trip.details[camelize($(this).text().replace(":", ""))] = ($(this).next().text() || "");
                obj.airline.name = trip.details["airline"];
            });
            if ($(this).next()[0] && $(this).next()[0].tagName == 'DIV') {
                var moreDetail = ($(this).next().text() || "");
                if (moreDetail.indexOf('LAY-OVER') >= 0) {
                    trip.layOver = (moreDetail.replace("LAY-OVER :", "") || "");
                }
                else if (moreDetail.indexOf('TOTAL FLIGHT TIME') >= 0) {
                    trip.totalFlightTime = moreDetail.split(":").slice(1).join(":");
                    if (obj.trips[obj.trips.length - 1] && obj.query.tripType >= 2) {
                        obj.trips[obj.trips.length - 1].totalFlightTime = moreDetail.split(":").slice(1).join(":");
                    }
                }
            }
            obj.trips.push(trip);
        });
        arr.push(obj);
    });
    if (queryType == 'string') {
        return JSON.stringify(arr);
    }
    return arr;
}