/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />

module ComicTales {

    export function Init() {

        var viewModel = new EditorViewModel();

        ko.applyBindings(viewModel);

    }

    export class EditorViewModel {

        public tiles = ko.observableArray([]);

        constructor() {

            this.tiles = ko.observableArray([
                new TileViewModel('aaa')
            ]);
        }

        public addNewTile(): void {
            new EditTileDialog().open();
        }


        public editTile(tile: TileViewModel) {
            new EditTileDialog(tile).open();
        }

        public deleteTile(tile: TileViewModel) {
            // todo: not implemented yet
        }
    }

    export class TileViewModel {

        public imageUrl = '/Content/imgs/tile1.png';

        constructor(public id: string) {
        }
    }
}