/**
 * @ngdoc service
 * @name merchello.models.extendedDataDisplayBuilder
 *
 * @description
 * A utility service that builds ExtendedDataBuilder models
 */
angular.module('merchello.models')
    .factory('extendedContentDisplayBuilder',
        ['genericModelBuilder', 'ExtendedDataDisplay', 'ExtendedDataItemDisplay',
            function(genericModelBuilder, ExtendedDataDisplay, ExtendedDataItemDisplay) {

                var Constructor = ExtendedDataDisplay;


                return {
                    createDefault: function() {
                        return new Constructor();
                    },
                    transform: function(jsonResult) {
                        var extendedData = new Constructor();
                        if (jsonResult !== undefined) {
                            var items = genericModelBuilder.transform(jsonResult, ExtendedDataItemDisplay);
                            if(items.length > 0) {
                                angular.forEach(items, function(i) {
                                    if (i.value !== null && i.value !== undefined && i.value !== '') {
                                        try {
                                            i.value = angular.fromJson(i.value);
                                        }
                                        catch(err) {
                                            i.value = i.value;
                                        }

                                    }

                                });
                                extendedData.items = items;
                            }
                        }
                        return extendedData;
                    }
                };
            }]);
