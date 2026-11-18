import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';

import { ConcertService } from '../../../core/services/concert.service';
import { CreateConcertRequest } from '../../../core/models/create-concert-request.model';
import { UpdateConcertRequest } from '../../../core/models/update-concert-request.model';

@Component({
  selector: 'app-concert-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './concert-form.html',
  styleUrl: './concert-form.css'
})
export class ConcertForm implements OnInit {

  // Inject the required services
  private fb = inject(FormBuilder);
  private concertService = inject(ConcertService);
  private route = inject(ActivatedRoute);
  router = inject(Router);

  // Stores the concert ID when editing
  concertId: number | null = null;

  // Controls the form title
  isEditMode = false;

  // Tracks API request status
  loading = false;
  errorMessage = '';
  successMessage = '';

  // Create the reactive form
  concertForm = this.fb.group({
    concertName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
    artistName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
    description: ['', Validators.maxLength(1000)],
    genre: ['', [Validators.required, Validators.maxLength(80)]],
    language: ['', [Validators.required, Validators.maxLength(50)]],
    imageUrl: ['', Validators.maxLength(500)],
    status: ['Draft', Validators.required]
  });

  ngOnInit(): void {

    // Read the ID from the URL
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.concertId = Number(id);
      this.isEditMode = true;
      this.loadConcert();
    }
  }

  // GET: Load existing concert details for editing
  loadConcert(): void {

    if (this.concertId === null) return;

    this.loading = true;

    this.concertService.getConcertById(this.concertId).subscribe({
      next: (concert) => {

        // Fill the form with existing concert data
        this.concertForm.patchValue({
          concertName: concert.concertName,
          artistName: concert.artistName,
          description: concert.description ?? '',
          genre: concert.genre,
          language: concert.language,
          imageUrl: concert.imageUrl ?? '',
          status: concert.status
        });

        this.loading = false;
      },

      error: (error) => {
        this.errorMessage = 'Unable to load concert details.';
        this.loading = false;
        console.error(error);
      }
    });
  }

  // POST or PUT depending on the mode
  onSubmit(): void {

    if (this.concertForm.invalid) {
      this.concertForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const formValue = this.concertForm.getRawValue();

    if (this.isEditMode && this.concertId !== null) {

      const request: UpdateConcertRequest = {
        concertName: formValue.concertName!,
        artistName: formValue.artistName!,
        description: formValue.description || null,
        genre: formValue.genre!,
        language: formValue.language!,
        imageUrl: formValue.imageUrl || null,
        status: formValue.status!
      };

      this.concertService.updateConcert(this.concertId, request).subscribe({
        next: () => {
          this.successMessage = 'Concert updated successfully!';
          this.loading = false;
          this.router.navigate(['/concerts']);
        },
        error: (error) => {
          this.errorMessage = 'Unable to update concert.';
          this.loading = false;
          console.error(error);
        }
      });

    } else {

      const request: CreateConcertRequest = {
        concertName: formValue.concertName!,
        artistName: formValue.artistName!,
        description: formValue.description || undefined,
        genre: formValue.genre!,
        language: formValue.language!,
        imageUrl: formValue.imageUrl || undefined,
        status: formValue.status!
      };

      this.concertService.createConcert(request).subscribe({
        next: () => {
          this.successMessage = 'Concert created successfully!';
          this.loading = false;
          this.router.navigate(['/concerts']);
        },
        error: (error) => {
          this.errorMessage = 'Unable to create concert.';
          this.loading = false;
          console.error(error);
        }
      });
    }
  }
}