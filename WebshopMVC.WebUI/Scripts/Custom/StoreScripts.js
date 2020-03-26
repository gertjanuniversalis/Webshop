ngApp.controller("ItemController", function ($scope) {
    $scope.InitField = function (fieldName) {
        $scope[fieldName] = 1;
    };
    $scope.SubmitDisabled = function (fieldName) {
        var disabled = $scope[fieldName] < 1;
        return disabled;
    };
    $scope.SubmitClass = function (fieldName) {
        if (!$scope.SubmitDisabled(fieldName)) {
            return "btn btn-primary";
        }
        else {
            return "btn btn-secondary disabled";
        } 
    };
});