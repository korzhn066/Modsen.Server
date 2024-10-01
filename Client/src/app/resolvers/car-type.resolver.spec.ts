import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { carTypeResolver } from './car-type.resolver';

describe('carTypeResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => carTypeResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
