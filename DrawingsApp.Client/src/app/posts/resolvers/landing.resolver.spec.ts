import { TestBed } from '@angular/core/testing';

import { LandingResolver } from './landing.resolver';

describe('LandingResolver', () => {
  let resolver: LandingResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(LandingResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
