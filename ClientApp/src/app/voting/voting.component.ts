import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Candidate } from '../_models/candidate';
import { environment } from '../../../src/environments/environment'
import { VotingService, AuthenticationService } from '../_services';
import { Router } from '@angular/router';

@Component({templateUrl: 'voting.component.html'})
export class VotingComponent implements OnInit {
  public candidates: Candidate[];

  constructor(
    private router: Router,
    private votingService: VotingService,
    private authenticationService: AuthenticationService
  ) { 
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) { 
      this.router.navigate(['/']);
  }
  }

  ngOnInit() { }

  getAllCandidate(){
    this.votingService.getAllCandidates();
  }
}
