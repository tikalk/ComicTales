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
                url: 'Story/emptyId/Create',
                type: 'post',
                data: { storyName: this.storyName },
                success: function (data) {
                    window.location.href = 'Story/' + data.id + '/Edit/';
                }
            });
        };
        return CreateStoryViewModel;
    })();
    ComicTales.CreateStoryViewModel = CreateStoryViewModel;
})(ComicTales || (ComicTales = {}));
