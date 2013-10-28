/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />

module ComicTales {

    export function InitStory() {

        var viewModel = new CreateStoryViewModel();

        ko.applyBindings(viewModel);

    }

    export class CreateStoryViewModel {

        public storyName: KnockoutObservable<string>;

        constructor() {
            this.storyName = ko.observable('');
        }

        public createStory(): void {

            $.ajax({
                url: '/Story/emptyId/Create',
                type: 'post',
                data: { Name: this.storyName() }, //Name is the ComicStory.cs property
                success: (data) => {
                    window.location.href = '/Story/' + data.id + '/Edit/';
                }
            });

        }
    }
}