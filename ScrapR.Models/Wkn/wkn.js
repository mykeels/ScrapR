function getFlightsData() 
{ 
	return JSON.stringify(angular.element("[ng-controller]").scope().flightmodeldata); 
}