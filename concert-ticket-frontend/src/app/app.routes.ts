
import { Routes } from '@angular/router';

import { ConcertList } from './features/concerts/concert-list/concert-list';
import { ConcertForm } from './features/concerts/concert-form/concert-form';

export const routes: Routes = [

  // Create concert
  {
    path: 'concerts/create',
    component: ConcertForm
  },

  // Edit concert
  {
    path: 'concerts/edit/:id',
    component: ConcertForm
  },

  // Display all concerts
  {
    path: 'concerts',
    component: ConcertList,
    pathMatch: 'full'
  },

  // Default route
  {
    path: '',
    redirectTo: 'concerts',
    pathMatch: 'full'
  }

];
