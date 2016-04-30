/**
 * @ngdoc models
 * @name productVariantDetachedContentDisplayBuilder
 *
 * @description
 * A utility factory that builds ProductVariantDetachedContentDisplay models
 */
angular.module('merchello.models').factory('productVariantDetachedContentDisplayBuilder',
    ['genericModelBuilder', 'detachedContentTypeDisplayBuilder', 'extendedContentDisplayBuilder', 'ProductVariantDetachedContentDisplay',
    function(genericModelBuilder, detachedContentTypeBuilder, extendedContentDisplayBuilder, ProductVariantDetachedContentDisplay) {

        var Constructor = ProductVariantDetachedContentDisplay;

        return {
            createDefault: function() {

                var content = new Constructor();
                content.detachedContentType = detachedContentTypeBuilder.createDefault();
                content.detachedDataValues = extendedContentDisplayBuilder.createDefault();

                return content;
            },
            transform: function(jsonResult) {
                var contents = [];
                if (angular.isArray(jsonResult)) {
                    for(var i = 0; i < jsonResult.length; i++) {
                        var content = genericModelBuilder.transform(jsonResult[ i ], Constructor);
                        content.detachedContentType = detachedContentTypeBuilder.transform(jsonResult[ i ].detachedContentType);
                        content.detachedDataValues = extendedContentDisplayBuilder.transform(jsonResult[ i ].detachedDataValues);
                        contents.push(content);
                    }
                } else {
                    contents = genericModelBuilder.transform(jsonResult, Constructor);
                    contents.detachedContentType = detachedContentTypeBuilder.transform(jsonResult.detachedContentType);
                    contents.detachedDataValues = extendedContentDisplayBuilder.transform(jsonResult.detachedDataValues);
                }
                return contents;
            }
        };
}]);
