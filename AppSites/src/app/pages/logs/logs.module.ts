import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LogsRoutingModule } from './logs-routing.module';
import { LogsComponent } from './logs.component';
import { ListComponent } from './list/list.component';
import { SearchComponent } from './search/search.component';
import { FormModule } from 'ng-devui/form';
import { TextInputModule } from 'ng-devui/text-input';
import { DatepickerModule } from 'ng-devui/datepicker';


@NgModule({
  declarations: [
    LogsComponent,
    ListComponent,
    SearchComponent
  ],
  imports: [
    CommonModule,
    LogsRoutingModule,
    FormModule,
    TextInputModule,
    DatepickerModule  
  ]
})
export class LogsModule { }
