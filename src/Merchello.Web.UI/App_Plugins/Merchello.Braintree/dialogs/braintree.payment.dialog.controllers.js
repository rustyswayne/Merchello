(function() {

    angular.module('merchello.plugins.braintree').controller('Merchello.Plugins.Braintree.Dialogs.Standard.AuthorizeCapturePaymentController',
        ['$scope', 'assetsService', 'invoiceHelper', 'braintreeResource', 'braintreeCreditCardBuilder', 'braintreeDialogHelper',
        function($scope, assetsService, invoiceHelper, braintreeResource, braintreeCreditCardBuilder, braintreeDialogHelper) {

            $scope.loaded = false;
            $scope.wasFormSubmitted = false;
            $scope.cardholderName = '';
            $scope.cardNumber = '';
            $scope.selectedMonth = {};
            $scope.expirationYear = '';
            $scope.cvv = '';
            $scope.postalCode = '';
            $scope.months = [];
            $scope.years = [];
            $scope.braintreeClient = {};

            // Exposed methods
            $scope.save = save;



            function init() {
                var filesToLoad = ['https://js.braintreegateway.com/v2/braintree.js'];
                $scope.dialogData.warning = 'All credit card information is tokenized. No values are passed to the server.';

                var billingAddress = $scope.dialogData.invoice.getBillToAddress();
                $scope.cardholderName = billingAddress.name;
                $scope.postalCode = billingAddress.postalCode;
                $scope.months = braintreeDialogHelper.getMonths();
                $scope.years = braintreeDialogHelper.getYears(15);
                assetsService.load(filesToLoad).then(function () {
                    setupBraintree();
                });
                $scope.dialogData.amount = invoiceHelper.round($scope.dialogData.invoiceBalance, 2);
            }

            function setupBraintree() {
                var promise = braintreeResource.getClientRequestToken($scope.dialogData.invoice.customerKey);
                promise.then(function(requestToken) {
                    var setup = {
                        clientToken: JSON.parse(requestToken)
                    };
                    $scope.braintreeClient = new braintree.api.Client(setup);
                    $scope.loaded = true;
                    console.info($scope.dialogData);
                });
            }

            function save() {
                $scope.wasFormSubmitted = true;
                if(invoiceHelper.valueIsInRage($scope.dialogData.amount, 0, $scope.dialogData.invoiceBalance) && $scope.authCaptureForm.cardholderName.$valid
                && $scope.authCaptureForm.cardNumber.$valid && $scope.authCaptureForm.cvv.$valid && $scope.authCaptureForm.postalCode.$valid) {

                    var cc = braintreeCreditCardBuilder.createDefault();
                    cc.cardholderName = $scope.cardholderName;
                    cc.number = $scope.cardNumber;
                    cc.cvv = $scope.cvv;
                    cc.expirationMonth = invoiceHelper.padLeft($scope.selectedMonth.monthNumber + 1, '0', 2);
                    cc.expirationYear = $scope.expirationYear;
                    cc.billingAddress.postalCode = $scope.postalCode;
                    $scope.dialogData.showSpinner();
                    $scope.braintreeClient.tokenizeCard(cc, function (err, nonce) {
                        // Send nonce to your server
                        if(err !== null) {
                            console.info(err);
                            return;
                        }
                        $scope.dialogData.processorArgs.setValue('nonce-from-the-client', nonce);
                        $scope.submit($scope.dialogData);
                    });
                } else {
                    if(!invoiceHelper.valueIsInRage($scope.dialogData.amount, 0, $scope.dialogData.invoiceBalance)) {
                        $scope.authCaptureForm.amount.$setValidity('amount', false);
                    }
                }
            }


            // Initialize the controller
            init();
        }]);

    angular.module('merchello.plugins.braintree').controller('Merchello.Plugins.Braintree.Dialogs.Standard.VoidPaymentController',
        ['$scope',
        function($scope) {

            function init() {
                $scope.dialogData.warning = 'Please note this will only void the store payment record and this DOES NOT pass any information onto Braintree.'
            }

            // initialize the controller
            init();
        }]);

    angular.module('merchello.plugins.braintree').controller('Merchello.Plugins.Braintree.Dialogs.Standard.CapturePaymentController',
        ['$scope', 'assetsService', 'invoiceHelper', 'braintreeResource', 'braintreeCreditCardBuilder', 'braintreeDialogHelper',
        function($scope, assetsService, invoiceHelper, braintreeResource, braintreeCreditCardBuilder, braintreeDialogHelper) {

            $scope.transaction = {};
            $scope.isRetry = false;
            $scope.loaded = false;
            $scope.wasFormSubmitted = false;
            $scope.cardholderName = '';
            $scope.cardNumber = '';
            $scope.selectedMonth = {};
            $scope.expirationYear = '';
            $scope.cvv = '';
            $scope.postalCode = '';
            $scope.months = [];
            $scope.years = [];
            $scope.braintreeClient = {};

            // Exposed methods
            $scope.save = save;

            function init() {
                $scope.dialogData.amount = invoiceHelper.round($scope.dialogData.invoiceBalance, 2);
                var transactionStr = $scope.dialogData.payment.extendedData.getValue('braintreeTransaction');
                if(transactionStr.length) { // this is indeed a previously authorized transaction
                    $scope.transaction = JSON.parse(transactionStr);
                    $scope.dialogData.warning = 'This action will submit a previously authorized transaction for settlement.';
                } else { // this is likely a failed transaction that should be reattempted
                    $scope.isRetry = true;
                    var filesToLoad = ['https://js.braintreegateway.com/v2/braintree.js'];
                    $scope.dialogData.warning = 'The previous transaction failed.  You will need to reenter the credit card information.  All credit card information is tokenized. No values are passed to the server.';
                    // force this to be a standard transaction.
                    $scope.dialogData.paymentMethodKey = 'f05ad471-8810-45df-9228-e63ffa801bbe';
                    var billingAddress = $scope.dialogData.invoice.getBillToAddress();
                    $scope.cardholderName = billingAddress.name;
                    $scope.postalCode = billingAddress.postalCode;
                    $scope.months = braintreeDialogHelper.getMonths();
                    $scope.years = braintreeDialogHelper.getYears(15);
                    assetsService.load(filesToLoad).then(function () {
                        setupBraintree();
                    });
                    $scope.dialogData.amount = invoiceHelper.round($scope.dialogData.invoiceBalance, 2);
                    //$scope.close();
                }
            }

            function setupBraintree() {
                var promise = braintreeResource.getClientRequestToken($scope.dialogData.invoice.customerKey);
                promise.then(function(requestToken) {
                    var setup = {
                        clientToken: JSON.parse(requestToken)
                    };
                    $scope.braintreeClient = new braintree.api.Client(setup);
                    $scope.loaded = true;
                    console.info($scope.dialogData);
                });
            }

            function save() {
                $scope.wasFormSubmitted = true;
                if(invoiceHelper.valueIsInRage($scope.dialogData.amount, 0, $scope.dialogData.invoiceBalance) && $scope.retryCaptureForm.cardholderName.$valid
                    && $scope.retryCaptureForm.cardNumber.$valid && $scope.retryCaptureForm.cvv.$valid && $scope.retryCaptureForm.postalCode.$valid) {

                    var cc = braintreeCreditCardBuilder.createDefault();
                    cc.cardholderName = $scope.cardholderName;
                    cc.number = $scope.cardNumber;
                    cc.cvv = $scope.cvv;
                    cc.expirationMonth = invoiceHelper.padLeft($scope.selectedMonth.monthNumber + 1, '0', 2);
                    cc.expirationYear = $scope.expirationYear;
                    cc.billingAddress.postalCode = $scope.postalCode;
                    $scope.dialogData.showSpinner();
                    $scope.braintreeClient.tokenizeCard(cc, function (err, nonce) {
                        // Send nonce to your server
                        if(err !== null) {
                            console.info(err);
                            return;
                        }
                        $scope.dialogData.processorArgs.setValue('nonce-from-the-client', nonce);
                        $scope.submit($scope.dialogData);
                    });
                } else {
                    if(!invoiceHelper.valueIsInRage($scope.dialogData.amount, 0, $scope.dialogData.invoiceBalance)) {
                        $scope.authCaptureForm.amount.$setValidity('amount', false);
                    }
                }
            }

            init();
            // initialize the controller
        }]);

    angular.module('merchello').controller('Merchello.Plugins.Braintree.Dialogs.Dialogs.RefundPaymentController',
        ['$scope', 'invoiceHelper',
            function($scope, invoiceHelper) {

                $scope.wasFormSubmitted = false;
                $scope.save = save;

                function init() {
                    $scope.dialogData.amount = invoiceHelper.round($scope.dialogData.appliedAmount, 2);
                    $scope.dialogData.warning = 'Please note this operation will refund process a refund with Braintree.';
                }

                function save() {
                    $scope.wasFormSubmitted = true;
                    if(invoiceHelper.valueIsInRage($scope.dialogData.amount, 0, $scope.dialogData.appliedAmount)) {
                        $scope.submit($scope.dialogData);
                    } else {
                        $scope.refundForm.amount.$setValidity('amount', false);
                    }
                }
                // initializes the controller
                init();
            }]);

}());