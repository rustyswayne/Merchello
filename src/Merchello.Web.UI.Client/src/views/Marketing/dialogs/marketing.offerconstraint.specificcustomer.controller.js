/**
 * @ngdoc controller
 * @name Merchello.Marketing.Dialogs.Dialogs.OfferConstraintSpecificCustomerController
 * @function
 *
 * @description
 * The controller to configure the line item quantity component constraint
 */
angular.module('merchello').controller('Merchello.Marketing.Dialogs.OfferConstraintSpecificCustomerController',
    ['$scope', 'customerResource', 'customerDisplayBuilder', 'queryDisplayBuilder', 'queryResultDisplayBuilder',
        function($scope, customerResource, customerDisplayBuilder, queryDisplayBuilder, queryResultDisplayBuilder) {

            $scope.loaded = false;
            $scope.customers = [];
            $scope.selectedCustomer = {};

            // exposed
            $scope.save = save;

            function init() {
                loadCustomers();
                if ($scope.dialogData.component.isConfigured()) {

                    loadExistingConfigurations();
                    $scope.loaded = true;
                } else {
                    $scope.loaded = true;
                }
            }

            function loadCustomers() {
                var query = queryDisplayBuilder.createDefault();
                query.currentPage = 0;
                query.itemsPerPage = 100;

                var customerPromise = customerResource.searchCustomers(query);
                customerPromise.then(function(results) {
                    var qr = queryResultDisplayBuilder.transform(results, customerDisplayBuilder);
                    $scope.customers =qr.items;

                    console.info($scope.customers)
                });
            }

            function loadExistingConfigurations() {
                var customerKey = $scope.dialogData.getValue('customerKey')
                $scope.customerKey = customerKey === '' ? '' : customerKey;
            }

            function save() {
                $scope.dialogData.setValue('customerKey', $scope.selectedCustomer.key);
                $scope.submit($scope.dialogData);
            }

            // Initialize the controller
            init();
        }]);
