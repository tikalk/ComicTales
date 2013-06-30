/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />
var ComicTales;
(function (ComicTales) {
    function InitStory() {
        var viewModel = new CreateStoryViewModel();
        ko.applyBindings(viewModel);
    }
    ComicTales.InitStory = InitStory;
    var CreateStoryViewModel = (function () {
        function CreateStoryViewModel() {
            this.storyName = ko.observable('');
        }
        CreateStoryViewModel.prototype.createStory = function () {
            $.ajax({
                url: '/Story/emptyId/Create',
                type: 'post',
                data: {
                    Name: this.storyName()
                },
                success: //Name is the ComicStory.cs property
                function (data) {
                    window.location.href = '/Story/' + data.id + '/Edit/';
                }
            });
        };
        return CreateStoryViewModel;
    })();
    ComicTales.CreateStoryViewModel = CreateStoryViewModel;    
})(ComicTales || (ComicTales = {}));
//@ sourceMappingURL=CreateStory.js.map
