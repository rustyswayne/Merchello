(function() {

angular.module('merchello.plugins.braintree').service('braintreeDialogHelper',
    function() {

        this.getMonths = function() {
            var months = [];
            var d = new Date();
            var monthNames = [ "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December" ];
            for(var i = 0; i < monthNames.length; i++) {
                months.push({ monthNumber: i,  monthName: monthNames[i] });
            }
            return months;
        }

        this.getYears = function(numPastCurrent) {
            var years = [];
            if(numPastCurrent === undefined) {
                numPastCurrent = 15;
            }
            var d = new Date();
            var start = d.getFullYear();
            for(var i = 0; i < numPastCurrent; i++) {
                years.push(start + i);
            }
            return years;
        }

    });

}());