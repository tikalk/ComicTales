/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

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
            alert('add');
        }


        public editTile(tile: TileViewModel) {
            alert('edit ' + tile.id);
        }

        public deleteTile(tile: TileViewModel) {
            alert('delete ' + tile.id);
        }
    }

    export class TileViewModel {

        constructor(public id: string) {
        }
    }
}