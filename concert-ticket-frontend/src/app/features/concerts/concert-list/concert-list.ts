
import { Component, inject, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { HttpErrorResponse } from '@angular/common/http';

import { ConcertService } from '../../../core/services/concert.service';

import { ConcertResponse } from '../../../core/models/concert-response.model';

import { PagedConcertResponse } from '../../../core/models/paged-concert-response.model';

@Component({
  selector: 'app-concert-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './concert-list.html',
  styleUrl: './concert-list.css'
})
export class ConcertList implements OnInit {

  private concertService = inject(ConcertService);

  // Search input
  searchControl = new FormControl('', {
    nonNullable: true
  });

  // Concert collection
  concerts: ConcertResponse[] = [];

  // Selected concert for details
  selectedConcert: ConcertResponse | null = null;

  // Pagination information
  totalCount = 0;

  pageNumber = 1;

  pageSize = 10;

  totalPages = 0;

  // UI states
  isLoading = false;

  errorMessage = '';

  ngOnInit(): void {
    this.loadConcerts();
  }

  // GET all concerts
  loadConcerts(page: number = 1): void {

    this.isLoading = true;

    this.errorMessage = '';

    this.pageNumber = page;

    this.concertService.getConcerts(
      this.searchControl.value,
      this.pageNumber,
      this.pageSize
    ).subscribe({

      next: (response: PagedConcertResponse) => {

        this.concerts = response.concerts;

        this.totalCount = response.totalCount;

        this.pageNumber = response.pageNumber;

        this.pageSize = response.pageSize;

        this.totalPages = response.totalPages;

        this.isLoading = false;

      },

      error: (error: HttpErrorResponse) => {

        console.error('Get Concerts Error:', error);

        this.errorMessage =
          error.error?.message ||
          (error.status === 0
            ? 'Cannot connect to the backend.'
            : 'Failed to load concerts.');

        this.isLoading = false;

      }

    });

  }

  // Search concerts
  searchConcerts(): void {
    this.loadConcerts(1);
  }

  // Clear search
  clearSearch(): void {

    this.searchControl.setValue('');

    this.loadConcerts(1);

  }

  // Go to previous page
  previousPage(): void {

    if (this.pageNumber > 1) {
      this.loadConcerts(this.pageNumber - 1);
    }

  }

  // Go to next page
  nextPage(): void {

    if (this.pageNumber < this.totalPages) {
      this.loadConcerts(this.pageNumber + 1);
    }

  }

  // GET concert by ID
  viewDetails(id: number): void {

    this.errorMessage = '';

    this.selectedConcert = null;

    this.concertService.getConcertById(id).subscribe({

      next: (concert: ConcertResponse) => {

        this.selectedConcert = concert;

      },

      error: (error: HttpErrorResponse) => {

        console.error('Get Concert By ID Error:', error);

        this.errorMessage =
          error.status === 404
            ? 'Concert not found.'
            : 'Unable to load concert details.';

      }

    });

  }

  // Close details
  closeDetails(): void {
    this.selectedConcert = null;
  }

}
