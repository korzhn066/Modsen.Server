import { TestBed } from '@angular/core/testing';

import { ProcessingCarsService } from './processing-cars.service';

describe('ProcessingCarsService', () => {
  let service: ProcessingCarsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcessingCarsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
