import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from '@angular/material';
import components from './components';

@NgModule({
    imports:
    [
        BrowserModule,
		MaterialModule
    ],
    declarations:
    [
		...components.declarations
    ],
    bootstrap: [components.bootstrap]
})
export class AppModule { }