import { TestBed } from '@angular/core/testing';

import { CarFactoryService } from './car-factory.service';

describe('CarFactoryService', () => {
  let service: CarFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
