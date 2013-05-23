/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="EditTileDialog.ts" />

module ComicTales {

    export function Init(storyId: string) {

        var viewModel = new EditorViewModel(storyId);

        ko.applyBindings(viewModel);

    }

    export class EditorViewModel {

        public tiles = ko.observableArray([]);
        public isLoading = ko.observable(false);
        public hasUpdates = ko.observable(false);

        constructor(private storyId: string) {
            this.loadTiles();
        }

        public refresh(): void {
            this.loadTiles();
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

        private loadTiles() {
            this.isLoading(true);
            $.get('/Story/' + this.storyId + '/GetTiles', (data, textStatus, jqXHR) => {
                this.isLoading(false);
                this.updateTiles(data);
            });
        }

        private updateTiles(data) {
            this.tiles.removeAll();
            $.each(data.tiles, (index, tile) => this.tiles.push(tile));
        }
    }

    export interface TileViewModel {

        id: string;
        imageUrl: string;
    }
}