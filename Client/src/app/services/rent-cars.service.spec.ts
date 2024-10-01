import { TestBed } from '@angular/core/testing';

import { RentCarsService } from './rent-cars.service';

describe('RentCarsService', () => {
  let service: RentCarsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentCarsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
