import { TestBed } from '@angular/core/testing';

import { ElectionCarsService } from './election-cars.service';

describe('ElectionCarsService', () => {
  let service: ElectionCarsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ElectionCarsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
